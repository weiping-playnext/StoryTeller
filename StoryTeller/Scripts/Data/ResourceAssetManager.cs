using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StoryTeller.Data
{
    public class ResourceAssetManager : IAssetSource
    {
        Dictionary<string, Object> assetURIToObjectReference = new Dictionary<string, Object>();
        Dictionary<string, string> assetTagToSourceReference = new Dictionary<string, string>();
        List<string> assetURI = new List<string>();

        public ResourceAssetManager()
        {
        }

        public void AddAssetURI(string uri)
        {
            if (!assetURI.Contains(uri)) assetURI.Add(uri);
        }

        public void AddTag(string uri, string tag)
        {
            AddAssetURI(uri);
            assetTagToSourceReference[tag] = uri;
        }

        public T GetAsset<T>(string tag) where T : Object
        {
            var uri = assetTagToSourceReference[tag];
            return Resources.Load<T>(uri);
        }

        #region IDisposable Support
        private bool disposed = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                assetURIToObjectReference.Clear();
                assetTagToSourceReference.Clear();
                Resources.UnloadUnusedAssets();
                System.GC.Collect();
            }

            // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
            // TODO: 大きなフィールドを null に設定します。

            disposed = true;
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~ResourceAssetManager() {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
