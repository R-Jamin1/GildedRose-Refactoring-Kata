using System.Collections.Generic;

namespace GildedRoseKata;


public class GildedRose
{
    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {

        foreach (Item item in Items)
        {
            //Montant dont va varier la qualité d'un produit à la fin de la journée
            int qualityVariation = 0;

            //Ce booleen sert à definir si on update la qualité d'un item ou non.
            //Ce serait un parfait attribut de la classe Item, mais le gobelin n'est pas d'accord
            bool fixedQuality = false;

            //Je traite d'abord les cas spécifiques puis le cas général pour simplifier les conditions dans les if et la lecture

            //ToLower pour eviter les problèmes de majuscule qui arriveront forcément avec des produits rentrés à la main
            if (item.Name.ToLower() == "Sulfuras, Hand of Ragnaros".ToLower())
            {
                //Pas de date de peremption pour Sulfuras
                fixedQuality = true;
            }
            else if (item.Name.ToLower() == "Aged Brie".ToLower())
            {
                item.SellIn--;
                qualityVariation = 1;                
            }
            //J'utilise un Contain pour pouvoir prendre en charge des backstage passes pour d'autres concerts que celui en exemple            
            else if (item.Name.ToLower().Contains("Backstage passes".ToLower()))
            {
                item.SellIn--;
                if (item.SellIn > 10)
                {
                    qualityVariation = 1;
                }
                else if (item.SellIn <= 10 && item.SellIn > 5)
                {
                    qualityVariation = 2;
                }
                else if (item.SellIn <= 5 && item.SellIn > 0)
                {
                    qualityVariation = 3;
                }
                else
                {
                    qualityVariation = 0;
                    item.Quality = 0;
                }
            }
            else
            {
                item.SellIn--;
                if (item.Name.ToLower().Contains("Conjured".ToLower()))
                {
                    qualityVariation = -2;
                }
                else
                {
                    qualityVariation = -1;
                }
            }

            // On ne touche pas aux items légendaires
            if (!fixedQuality)
            {
                //On applique le changement en prenant en compte les limites autorisées
                item.Quality += qualityVariation;

                if (item.Quality < 0)
                {
                    item.Quality = 0;
                }

                if (item.Quality > 50)
                {
                    item.Quality = 50;
                }                
            }

        }
    }
}