using System.Collections.Generic;
using Sienar.Unity.Core.Singleton;
using Sienar.TalkieRobie.DataCenter;
namespace Sienar.TalkieRobie.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public delegate void GameManagerEvent(List<CatalogModel> catalogModels);
        event GameManagerEvent onLoadData;

        List<CatalogModel> catalogModels;

        public void SetCatalog(List<CatalogModel> catalogs)
        {
            if (catalogs == null)
                return;
            catalogModels = catalogs;
            onLoadData?.Invoke(catalogModels);
        }
        public List<CatalogModel> GetCatalog() 
        {
            return catalogModels;
        }
        public void BindOnLoadData(GameManagerEvent gameManager)
        {
            if (gameManager == null)
                return;
            onLoadData += gameManager;
        }
        public void UnBindOnLoadData(GameManagerEvent gameManager)
        {
            if (gameObject != null)
                return;
            onLoadData -= gameManager;
        }
    }
}