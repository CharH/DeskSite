using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DeskTopper
{
    class DeskOrderMethods
    {
        static int MIN_VALUE_DeskSize = 12;
        static int MAX_VALUE_DeskSize = 500;
        string errorMessage;


        public void CalcTotalCost(Desk desk)
        {
            desk.priceEstimate = CalcAreaCost(desk) + CalcDrawerCost(desk) + CalcMaterialCost(desk) + CalcRushOrderCost(desk);
        }

        private int CalcDrawerCost(Desk desk)
        {
            int cost = desk.noOfDrawers * 25;
            return cost;
        }

        //determine rush order pricing using size of desk and rush order timing.
        private int CalcRushOrderCost(Desk desk)
        {
            int deskArea = getArea(desk.deskWidth, desk.deskLength);
            int rushCost = 0;
            int i = 0, j = 0;


            if (desk.rushOrder.Equals(0))
                return 0;
            else
            {
                //use desk area to set i
                if (deskArea <= 1000)
                    j = 0;
                else if ((deskArea > 1000) && (deskArea < 2000))
                    j = 1;
                else
                    j = 2;
                //use rush order timing to set j
                switch (desk.rushOrder)
                {
                    case 3:
                        i = 0;
                        break;
                    case 5:
                        i = 1;
                        break;
                    case 7:
                        i = 2;
                        break;
                }

                //read in rush order pricing file
                int[,] rushOrderPricing = new int[3, 3];
                GetRushOrderInfo(rushOrderPricing);

                //use i and j values to pull up appropriate pricing from 2D array
                rushCost = rushOrderPricing[i, j];

                return rushCost;
            }

        }

        private int getArea(int width, int length)
        {
            int area = width * length;
            return area;
        }

        //determine material price based on user selection and return price
        private int CalcMaterialCost(Desk desk)
        {
            int price = 0;
            switch (desk.deskTopType)
            {
                case SurfaceMaterial.Oak:
                    price = 200;
                    break;
                case SurfaceMaterial.Laminate:
                    price = 100;
                    break;
                case SurfaceMaterial.Pine:
                    price = 50;
                    break;
                case SurfaceMaterial.Marble:
                    price = 500;
                    break;
                case SurfaceMaterial.Walnut:
                    price = 250;
                    break;
                case SurfaceMaterial.Metal:
                    price = 300;
                    break;
                default:
                    errorMessage += "Invalid material choice detected. ";
                    return 0;
            }
            return price;
        }

        //calculate a price based on desk surface area and return price
        private int CalcAreaCost(Desk desk)
        {
            int deskArea = getArea(desk.deskWidth, desk.deskLength);
            int price = 0;
            if (deskArea > 1000)
            {
                price = 5 * (deskArea - 1000) + 200;
            }
            else
                price = 200;
            return price;
        }

        //read in pricing info file and place it into a 2D array
        private void GetRushOrderInfo(int[,] array)
        {
            try
            {
                string[] lines = File.ReadAllLines(@"rushOrderPrices.txt");
                int count = 0;
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        array[i, j] = int.Parse(lines[count]);
                        count++;
                    }
                }


            }
            catch (Exception e)
            {
                errorMessage += e.Message;
            }
        }

    }
}
