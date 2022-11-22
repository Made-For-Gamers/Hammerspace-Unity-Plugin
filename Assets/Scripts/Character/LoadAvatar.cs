using UnityEngine;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using GLTFast.Loading;
using GLTFast.Logging;
using GLTFast.Materials;

namespace GLTFast
{
    public class LoadAvatar : GltfAssetBase
    {
        [Tooltip("URL to load the glTF from.")]
        public string url;

        [Tooltip("Automatically load at start.")]
        public bool loadOnStartup = true;

        [Tooltip("Override scene to load (-1 loads glTFs default scene)")]
        public int sceneId = -1;

        [Tooltip("If checked, url is treated as relative StreamingAssets path.")]
        public bool streamingAsset = false;


        /// Latest scene's instance.
        public GameObjectInstantiator.SceneInstance sceneInstance { get; protected set; }

        public string FullUrl => streamingAsset
            ? Path.Combine(Application.streamingAssetsPath, url)
            : url;

        protected virtual async void Start()
        {
            if (loadOnStartup && !string.IsNullOrEmpty(url))
            {
                // Automatic load on startup
                await Load(FullUrl);
            }
        }

        public async void LoadTheAvatar(string newUrl)
        {
            url = newUrl;
            await Load(FullUrl);
        }

        public override async Task<bool> Load(
            string url,
            IDownloadProvider downloadProvider = null,
            IDeferAgent deferAgent = null,
            IMaterialGenerator materialGenerator = null,
            ICodeLogger logger = null
            )
        {
            logger = logger ?? new ConsoleLogger();
            var success = await base.Load(url, downloadProvider, deferAgent, materialGenerator, logger);
            if (success)
            {
                if (deferAgent != null) await deferAgent.BreakPoint();
                // Auto-Instantiate
                if (sceneId >= 0)
                {
                    InstantiateScene(sceneId, logger);
                }
                else
                {
                    Instantiate(logger);
                }
            }
            return success;
        }

        protected override IInstantiator GetDefaultInstantiator(ICodeLogger logger)
        {
            return new GameObjectInstantiator(importer, transform, logger);
        }

        protected override void PostInstantiation(IInstantiator instantiator, bool success)
        {
            sceneInstance = (instantiator as GameObjectInstantiator).sceneInstance;
            base.PostInstantiation(instantiator, success);
        }

        /// Removes previously instantiated scene(s)
        public override void ClearScenes()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            sceneInstance = null;
        }

        ////Reset animator
        //IEnumerator RebindAnimator()
        //{
        //    print("animatator rebind");
        //    animator.enabled = false;
        //    yield return new WaitForSeconds(0.1f);
        //    animator.enabled = true;
        //    animator.Rebind();
        //}
    }
}
