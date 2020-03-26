using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Models;
using Training1.Repositories;

namespace Training1.Data
{
    public static class ProductSeedData
    {
        #region Ensure populated
        public static void EnsurePopulated(
            ProductContext productContext,
            AppIdentityContext identityContext)
        {
            if (!productContext.Product.Any())
            {
                CreateProducts(productContext)
                    .GetAwaiter()
                    .GetResult();
            }
            if (!productContext.Sesshin.Any())
            {
                CreateSesshin(productContext, identityContext)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        private static async Task CreateProducts(ProductContext productContext)
        {
            await AddProductAndStockToDB(new Product { Name = "Carot", Category = ProductCategory.Vegetable, Description = "Vegetable orange, nice for salads or juices" }, 
                                            null, 
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Tomato", Category = ProductCategory.Vegetable, Description = "My favortie vegetable (I knooowwww, it's a fruit)" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Onion", Category = ProductCategory.Vegetable, Description = "Try not to cry :)" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Garlic", Category = ProductCategory.Vegetable, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Potato", Category = ProductCategory.Vegetable, Description = "French fries baby!!!!!!!!!" },
                                            null, 
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Apple", Category = ProductCategory.Fruit, Description = "Apple Pie, yummmmmmmmmmmmmmmmmy" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Bread", Category = ProductCategory.Cereal, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Butter", Category = ProductCategory.Condiment, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Strawberry jam", Category = ProductCategory.Condiment, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Banana", Category = ProductCategory.Fruit, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Coffee", Category = ProductCategory.Coffee, Description = "I want my coffee!!!!!!!!" },
                                            new Stock { Quantity = 1M, UnityType = UnityType.Kilogrammes, PricePorUnity = 4.76M, TotalPrice = 4.76M, Currency = Currency.Euro, CommandDate = DateTime.Now.AddYears(-1) },
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Rice", Category = ProductCategory.Cereal, Description = "My favorite cereal" },
                                            new Stock { Quantity = 2M, UnityType = UnityType.Kilogrammes, PricePorUnity = 5.04M, TotalPrice = 10.08M, Currency = Currency.Euro, CommandDate = DateTime.Now.AddYears(-1) },
                                            productContext);//Brown rice
            await AddProductAndStockToDB(new Product { Name = "Brown rice", Category = ProductCategory.Cereal, Description = "Genmai rice" },
                                            new Stock { Quantity = 10M, UnityType = UnityType.Kilogrammes, PricePorUnity = 3.55M, TotalPrice = 35.50M, Currency = Currency.Euro, CommandDate = DateTime.Now.AddYears(-1) },
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Quinoa", Category = ProductCategory.Cereal, Description = "Gluten freeeeeeeeeeeeeeeeeeeeeeeeeee" },
                                            new Stock { Quantity = 2M, UnityType = UnityType.Kilogrammes, PricePorUnity = 7.98M, TotalPrice = 15.96M, Currency = Currency.Euro, CommandDate = DateTime.Now.AddYears(-1) },
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Mustard", Category = ProductCategory.Condiment, Description = "Warning, very spicy!!!!!!!!!!!!" },
                                            new Stock { Quantity = 0.44M, UnityType = UnityType.Kilogrammes, PricePorUnity = 1.52M, TotalPrice = 0.67M, Currency = Currency.Euro, CommandDate = DateTime.Now.AddYears(-1) },
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Green Tea", Category = ProductCategory.Coffee, Description = "For people who doesn't like coffee" },
                                            new Stock { Quantity = 0.045M, UnityType = UnityType.Kilogrammes, PricePorUnity = 105.78M, TotalPrice = 4.76M, Currency = Currency.Euro, CommandDate = DateTime.Now.AddYears(-1) },
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Olive oil", Category = ProductCategory.Condiment, Description = "" },
                                            new Stock { Quantity = 5M, UnityType = UnityType.Liter, PricePorUnity = 5.89M, TotalPrice = 29.45M, Currency = Currency.Euro, CommandDate = DateTime.Now.AddYears(-1) },
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Split Pea", Category = ProductCategory.Legume, Description = "Rich source of vegetal protein"},
                                            new Stock { Quantity = 2M, UnityType = UnityType.Kilogrammes, PricePorUnity = 1.98M, TotalPrice = 3.96M, Currency = Currency.Euro, CommandDate = DateTime.Now.AddYears(-1) },
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Black lentil", Category = ProductCategory.Legume, Description = "" },
                                            new Stock { Quantity = 4M, UnityType = UnityType.Kilogrammes, PricePorUnity = 7.40M, TotalPrice = 29.60M, Currency = Currency.Euro, CommandDate = DateTime.Now.AddYears(-1) },
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Chocolate soya dessert", Category = ProductCategory.Else, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Curry", Category = ProductCategory.Condiment, Description = "Hot indian curry" },
                                            new Stock { Quantity = 0.050M, UnityType = UnityType.Kilogrammes, PricePorUnity = 52M, TotalPrice = 2.60M, Currency = Currency.Euro, CommandDate = DateTime.Now.AddYears(-1) },
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Pepper", Category = ProductCategory.Vegetable, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Eggplant", Category = ProductCategory.Vegetable, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Zucchini", Category = ProductCategory.Vegetable, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Egg", Category = ProductCategory.Else, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Brocoli", Category = ProductCategory.Vegetable, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Spaghetti", Category = ProductCategory.Cereal, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Cauliflower", Category = ProductCategory.Vegetable, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Curry", Category = ProductCategory.Condiment, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Flour", Category = ProductCategory.Else, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Milk", Category = ProductCategory.Else, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Squash", Category = ProductCategory.Vegetable, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Fresh Cream", Category = ProductCategory.Else, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Picnic Cheese", Category = ProductCategory.Else, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Lettuce", Category = ProductCategory.Vegetable, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Gluten flour", Category = ProductCategory.Else, Description = "Flour to make seitan" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Soy sauce", Category = ProductCategory.Else, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Peanut", Category = ProductCategory.Else, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Sugar", Category = ProductCategory.Else, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Limon", Category = ProductCategory.Vegetable, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Lasagna Noodle", Category = ProductCategory.Else, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Orange", Category = ProductCategory.Fruit, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Pear", Category = ProductCategory.Fruit, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Kiwifruit", Category = ProductCategory.Fruit, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Chocolate soya dessert", Category = ProductCategory.Else, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Leek", Category = ProductCategory.Vegetable, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Turnip", Category = ProductCategory.Vegetable, Description = "" },
                                            null,
                                            productContext);
            await AddProductAndStockToDB(new Product { Name = "Celery", Category = ProductCategory.Vegetable, Description = "" },
                                            null,
                                            productContext);
        }

        private static async Task CreateSesshin(ProductContext productContext, AppIdentityContext identityContext)
        {
            AppUser chef = identityContext.Users.FirstOrDefault(usr => usr.Email == "jimmy.chef@myapp.com");
            Sesshin newYear = new Sesshin { AppUserId = chef.Id, Name = "New Year 2019", Description = "Until the end of the night!!!", StartDate = new DateTime(2019, 12, 28), EndDate = new DateTime(2020, 01, 01), NumberOfPeople = 90 };
            //Create the genmai food that will be used after
            await AddGenMaiFood(productContext);
            EFSesshinRepository sesshinRepo = new EFSesshinRepository(productContext, new EFFoodRepository(productContext), new EFDayOfSesshinRepository(productContext));
            try
            {
                await sesshinRepo.AddAsync(newYear);
            }
            catch (Exception ex)
            {

            }
            
            //Add Food For Sesshins
            await CreateNewYearMeal(newYear, productContext);
        }
        #endregion

        #region Populate newYear sesshin days
        private static async Task CreateNewYearMeal(Sesshin newYearSesshin, ProductContext productContext)
        {
            await CompleteComingDayDiner(newYearSesshin, productContext);
            await CompleteFirstPreparationDay(newYearSesshin, productContext);
            await CompleteSecondPreparationDay(newYearSesshin, productContext);
            await CompleteSesshinDay(newYearSesshin, productContext);
            await CompleteRestDay(newYearSesshin, productContext);
            await CompleteLastDay(newYearSesshin, productContext);
        }

        private static async Task CompleteFirstPreparationDay(Sesshin newYearSesshin, ProductContext productContext)
        {
            //First Meal: Split pea soup + potatoes
            DayOfSesshin firstDayPreparation = newYearSesshin.Days.FirstOrDefault(d => d.Date == new DateTime(2019, 12, 28));
            Meal breakfast = CompleteFirstDayPreparationBreakfast(firstDayPreparation, productContext);
            Meal lunch = CompleteFirstDayPreparationLunch(firstDayPreparation, productContext);
            Meal diner = CompleteFirstDayPreparationDiner(firstDayPreparation, productContext);

            await productContext.SaveChangesAsync();
        }

        private static async Task CompleteSecondPreparationDay(Sesshin newYearSesshin, ProductContext productContext)
        {
            DayOfSesshin secondDayPreparation = newYearSesshin.Days.FirstOrDefault(d => d.Date == new DateTime(2019, 12, 29));
            Meal lunch = CompleteSecondDayPreparationLunch(secondDayPreparation, productContext);
            Meal diner = CompleteSecondDayPreparationDiner(secondDayPreparation, productContext);

            await productContext.SaveChangesAsync();
        }

        private static async Task CompleteSesshinDay(Sesshin newYearSesshin, ProductContext productContext)
        {
            DayOfSesshin sesshinDay = newYearSesshin.Days.FirstOrDefault(d => d.Date == new DateTime(2019, 12, 30));
            Meal lunch = CompleteSesshinDayLunch(sesshinDay, productContext);
            Meal diner = CompleteSesshinDayDiner(sesshinDay, productContext);

            await productContext.SaveChangesAsync();
        }

        private static async Task CompleteRestDay(Sesshin newYearSesshin, ProductContext productContext)
        {
            //First Meal: Split pea soup + potatoes
            DayOfSesshin restDay = newYearSesshin.Days.FirstOrDefault(d => d.Date == new DateTime(2019, 12, 31));
            Meal breakfast = CompleteRestDayBreakfast(restDay, productContext);
            Meal lunch = CompleteRestDayLunch(restDay, productContext);
            Meal diner = CompleteRestDayDiner(restDay, productContext);

            await productContext.SaveChangesAsync();
        }

        private static async Task CompleteLastDay(Sesshin newYearSesshin, ProductContext productContext)
        {
            //First Meal: Split pea soup + potatoes
            DayOfSesshin lastDay = newYearSesshin.Days.FirstOrDefault(d => d.Date == new DateTime(2020, 01, 01));
            Meal breakfast = CompleteLastDayBreakfast(lastDay, productContext);
            Meal lunch = CompleteLastDayLunch(lastDay, productContext);

            await productContext.SaveChangesAsync();
        }
        #endregion

        #region Complete days meals methods
        private static async Task CompleteComingDayDiner(Sesshin newYearSesshin, ProductContext productContext)
        {
            //First Meal: Split pea soup + potatoes
            DayOfSesshin comingDay = newYearSesshin.Days.FirstOrDefault(d => d.Date == new DateTime(2019, 12, 27));
            Meal comingDayDiner = comingDay.Meals.FirstOrDefault(m => m.Type == MealType.Diner);
            comingDayDiner.MealFoods = new List<MealFood>();

            comingDayDiner.MealFoods.Add(new MealFood { Meal = comingDayDiner, Food = SplitPeaSoup(productContext) });
            comingDayDiner.MealFoods.Add(new MealFood { Meal = comingDayDiner, Food = RoastedPotatoes(productContext) });
            AddSimpleFoodToMeal("Banana", 90M, UnityType.Unity, comingDayDiner, productContext);

            await productContext.SaveChangesAsync();
        }

        private static Meal CompleteFirstDayPreparationBreakfast(DayOfSesshin firstDayPreparation, ProductContext productContext)
        {
            Meal breakfast = firstDayPreparation.Meals.FirstOrDefault(m => m.Type == MealType.Genmai);
            breakfast.Type = MealType.Breakfast;
            breakfast.MealFoods = new List<MealFood>();
            AddSimpleFoodToMeal("Coffee", 3M, UnityType.Kilogrammes, breakfast, productContext);
            breakfast.MealFoods.Add(new MealFood { Meal = breakfast, Food = BreadButterAndJam(productContext) });

            return breakfast;
        }

        private static Meal CompleteFirstDayPreparationLunch(DayOfSesshin firstDayPreparation, ProductContext productContext)
        {
            Meal lunch = firstDayPreparation.Meals.FirstOrDefault(m => m.Type == MealType.Lunch);
            lunch.MealFoods = new List<MealFood>();
            lunch.MealFoods.Add(new MealFood { Meal = lunch, Food = Ratatouille(productContext) });
            AddSimpleFoodToMeal("Rice", 3.6M, UnityType.Kilogrammes, lunch, productContext);
            AddSimpleFoodToMeal("Apple", 90M, UnityType.Unity, lunch, productContext);

            return lunch;
        }

        private static Meal CompleteFirstDayPreparationDiner(DayOfSesshin firstDayPreparation, ProductContext productContext)
        {
            Meal diner = firstDayPreparation.Meals.FirstOrDefault(m => m.Type == MealType.Diner);
            diner.MealFoods = new List<MealFood>();
            diner.MealFoods.Add(new MealFood { Meal = diner, Food = Tortillas(productContext) });
            AddSimpleFoodToMeal("Chocolate soya dessert", 90M, UnityType.Unity, diner, productContext);

            return diner;
        }

        private static Meal CompleteSecondDayPreparationLunch(DayOfSesshin secondDayPreparation, ProductContext productContext)
        {
            Meal lunch = secondDayPreparation.Meals.FirstOrDefault(m => m.Type == MealType.Lunch);
            lunch.MealFoods = new List<MealFood>();
            lunch.MealFoods.Add(new MealFood { Meal = lunch, Food = SpaghettiWithBrocoliSauce(productContext) });
            AddSimpleFoodToMeal("Kiwifruit", 90M, UnityType.Unity, lunch, productContext);

            return lunch;
        }

        private static Meal CompleteSecondDayPreparationDiner(DayOfSesshin secondDayPreparation, ProductContext productContext)
        {
            Meal diner = secondDayPreparation.Meals.FirstOrDefault(m => m.Type == MealType.Diner);
            diner.MealFoods = new List<MealFood>();
            diner.MealFoods.Add(new MealFood { Meal = diner, Food = MixedVegetableCurry(productContext) });
            diner.MealFoods.Add(new MealFood { Meal = diner, Food = productContext.Food.FirstOrDefault(f => f.Name == "Rice") });
            AddSimpleFoodToMeal("Orange", 90M, UnityType.Unity, diner, productContext);

            return diner;
        }

        private static Meal CompleteSesshinDayLunch(DayOfSesshin sesshinDay, ProductContext productContext)
        {
            Meal lunch = sesshinDay.Meals.FirstOrDefault(m => m.Type == MealType.Lunch);
            lunch.MealFoods = new List<MealFood>();
            lunch.MealFoods.Add(new MealFood { Meal = lunch, Food = CauliFlowerGratin(productContext) });
            AddSimpleFoodToMeal("Pear", 90M, UnityType.Unity, lunch, productContext);

            return lunch;
        }

        private static Meal CompleteSesshinDayDiner(DayOfSesshin sesshinDay, ProductContext productContext)
        {
            Meal diner = sesshinDay.Meals.FirstOrDefault(m => m.Type == MealType.Diner);
            diner.MealFoods = new List<MealFood>();
            diner.MealFoods.Add(new MealFood { Meal = diner, Food = SquashSoup(productContext) });
            diner.MealFoods.Add(new MealFood { Meal = diner, Food = productContext.Food.FirstOrDefault(f => f.Name == "Apple") });

            return diner;
        }

        private static Meal CompleteRestDayBreakfast(DayOfSesshin restDay, ProductContext productContext)
        {
            Meal breakfast = restDay.Meals.FirstOrDefault(m => m.Type == MealType.Genmai);
            breakfast.MealFoods = new List<MealFood>();
            breakfast.Type = MealType.Breakfast;
            breakfast.MealFoods.Add(new MealFood { Food = productContext.Food.FirstOrDefault(f => f.Name == "Coffee"), Meal = breakfast });
            breakfast.MealFoods.Add(new MealFood { Food = productContext.Food.FirstOrDefault(f => f.Name == "Bread, butter and jelly"), Meal = breakfast });

            return breakfast;
        }

        private static Meal CompleteRestDayLunch(DayOfSesshin restDay, ProductContext productContext)
        {
            Meal lunch = restDay.Meals.FirstOrDefault(m => m.Type == MealType.Lunch);
            lunch.MealFoods = new List<MealFood>();
            lunch.MealFoods.Add(new MealFood { Meal = lunch, Food = BasicPicNic(productContext) });
            lunch.MealFoods.Add(new MealFood { Meal = lunch, Food = productContext.Food.FirstOrDefault(f => f.Name == "Banana") });

            return lunch;
        }

        private static Meal CompleteRestDayDiner(DayOfSesshin restDay, ProductContext productContext)
        {
            Meal diner = restDay.Meals.FirstOrDefault(m => m.Type == MealType.Diner);
            diner.MealFoods = new List<MealFood>();
            diner.MealFoods.Add(new MealFood { Meal = diner, Food = MafeSeitan(productContext) });
            diner.MealFoods.Add(new MealFood { Meal = diner, Food = productContext.Food.FirstOrDefault(f => f.Name == "Rice") });
            diner.MealFoods.Add(new MealFood { Meal = diner, Food = LimonPie(productContext) });

            return diner;
        }

        private static Meal CompleteLastDayBreakfast(DayOfSesshin lastDay, ProductContext productContext)
        {
            Meal breakfast = lastDay.Meals.FirstOrDefault(m => m.Type == MealType.Genmai);
            breakfast.MealFoods = new List<MealFood>();
            breakfast.Type = MealType.Breakfast;
            breakfast.MealFoods.Add(new MealFood { Food = productContext.Food.FirstOrDefault(f => f.Name == "Coffee"), Meal = breakfast});
            breakfast.MealFoods.Add(new MealFood { Food = productContext.Food.FirstOrDefault(f => f.Name == "Bread, butter and jelly"), Meal = breakfast });

            return breakfast;
        }
        
        private static Meal CompleteLastDayLunch(DayOfSesshin lastDay, ProductContext productContext)
        {
            Meal lunch = lastDay.Meals.FirstOrDefault(m => m.Type == MealType.Lunch);
            lunch.MealFoods = new List<MealFood>();
            lunch.MealFoods.Add(new MealFood { Meal = lunch, Food = VegetarianLasagna(productContext) });
            lunch.MealFoods.Add(new MealFood { Meal = lunch, Food = FruitSalad(productContext) });

            return lunch;
        }
        #endregion

        #region recipes methods
        private static Food SplitPeaSoup(ProductContext productContext)
        {
            Food splitPeaSoup = new Food { Name = "Split pea soup", NumberOfPeople = 90};
            splitPeaSoup.Ingredients = new List<Ingredient>();
            int carotsId = productContext.Product.FirstOrDefault(p => p.Name == "Carot").Id;
            splitPeaSoup.Ingredients.Add(new Ingredient { ProductId = carotsId, Quantity = 1M, UnityType = UnityType.Kilogrammes });
            int spleatPeaId = productContext.Product.FirstOrDefault(p => p.Name == "Split Pea").Id;
            splitPeaSoup.Ingredients.Add(new Ingredient { ProductId = spleatPeaId, Quantity = 3.6M, UnityType = UnityType.Kilogrammes });
            int onionsId = productContext.Product.FirstOrDefault(p => p.Name == "Onion").Id;
            splitPeaSoup.Ingredients.Add(new Ingredient { ProductId = onionsId, Quantity = 1M, UnityType = UnityType.Kilogrammes });

            return splitPeaSoup;
        }

        private static Food RoastedPotatoes(ProductContext productContext)
        {
            Food roastedPotatoes = new Food { Name = "Roasted potatoes", NumberOfPeople = 90 };
            roastedPotatoes.Ingredients = new List<Ingredient>();
            int potatoesId = productContext.Product.FirstOrDefault(p => p.Name == "Potato").Id;
            roastedPotatoes.Ingredients.Add(new Ingredient { ProductId = potatoesId, Quantity = 15M, UnityType = UnityType.Kilogrammes });
            int garlicId = productContext.Product.FirstOrDefault(p => p.Name == "Garlic").Id;
            roastedPotatoes.Ingredients.Add(new Ingredient { ProductId = garlicId, Quantity = 1M, UnityType = UnityType.Unity });

            return roastedPotatoes;
        }

        private static Food BreadButterAndJam(ProductContext productContext)
        {
            Food breadButterAndJam = new Food { Name = "Bread, butter and jelly", NumberOfPeople = 90 };
            breadButterAndJam.Ingredients = new List<Ingredient>();
            int breadId = productContext.Product.FirstOrDefault(p => p.Name == "Bread").Id;
            breadButterAndJam.Ingredients.Add(new Ingredient { ProductId = breadId, Quantity = 9M, UnityType = UnityType.Kilogrammes });
            int butterId = productContext.Product.FirstOrDefault(p => p.Name == "Butter").Id;
            breadButterAndJam.Ingredients.Add(new Ingredient { ProductId = butterId, Quantity = 1M, UnityType = UnityType.Kilogrammes });
            int jamId = productContext.Product.FirstOrDefault(p => p.Name == "Strawberry jam").Id;
            breadButterAndJam.Ingredients.Add(new Ingredient { ProductId = jamId, Quantity = 2M, UnityType = UnityType.Kilogrammes });

            return breadButterAndJam;
        }

        private static Food Ratatouille(ProductContext productContext)
        {
            Food ratatouille = new Food { Name = "Ratatouille", NumberOfPeople = 90 };
            ratatouille.Ingredients = new List<Ingredient>();
            int tomatoId = productContext.Product.FirstOrDefault(p => p.Name == "Tomato").Id;
            ratatouille.Ingredients.Add(new Ingredient { ProductId = tomatoId, Quantity = 10M, UnityType = UnityType.Kilogrammes });
            int onionId = productContext.Product.FirstOrDefault(p => p.Name == "Onion").Id;
            ratatouille.Ingredients.Add(new Ingredient { ProductId = onionId, Quantity = 5M, UnityType = UnityType.Kilogrammes });
            int pepperId = productContext.Product.FirstOrDefault(p => p.Name == "Pepper").Id;
            ratatouille.Ingredients.Add(new Ingredient { ProductId = pepperId, Quantity = 10M, UnityType = UnityType.Kilogrammes });
            int eggplantId = productContext.Product.FirstOrDefault(p => p.Name == "Eggplant").Id;
            ratatouille.Ingredients.Add(new Ingredient { ProductId = eggplantId, Quantity = 10M, UnityType = UnityType.Kilogrammes });
            int zucchiniId = productContext.Product.FirstOrDefault(p => p.Name == "Zucchini").Id;
            ratatouille.Ingredients.Add(new Ingredient { ProductId = zucchiniId, Quantity = 10M, UnityType = UnityType.Kilogrammes });

            return ratatouille;
        }

        private static Food Tortillas(ProductContext productContext)
        {
            Food tortilla = new Food { Name = "Tortillas", NumberOfPeople = 90 };
            tortilla.Ingredients = new List<Ingredient>();
            int eggsId = productContext.Product.FirstOrDefault(p => p.Name == "Egg").Id;
            tortilla.Ingredients.Add(new Ingredient { ProductId = eggsId, Quantity = 180M, UnityType = UnityType.Unity });
            int onionId = productContext.Product.FirstOrDefault(p => p.Name == "Onion").Id;
            tortilla.Ingredients.Add(new Ingredient { ProductId = onionId, Quantity = 2M, UnityType = UnityType.Kilogrammes });
            int potatoesId = productContext.Product.FirstOrDefault(p => p.Name == "Potato").Id;
            tortilla.Ingredients.Add(new Ingredient { ProductId = potatoesId, Quantity = 15M, UnityType = UnityType.Kilogrammes });
            int garlicId = productContext.Product.FirstOrDefault(p => p.Name == "Garlic").Id;
            tortilla.Ingredients.Add(new Ingredient { ProductId = garlicId, Quantity = 2M, UnityType = UnityType.Unity });

            return tortilla;
        }

        private static Food SpaghettiWithBrocoliSauce(ProductContext productContext)
        {
            Food spaghettiWithBrocoliSauce = new Food { Name = "Spaghetti with brocoli sauce", NumberOfPeople = 90 };
            spaghettiWithBrocoliSauce.Ingredients = new List<Ingredient>();
            int garlicId = productContext.Product.FirstOrDefault(p => p.Name == "Garlic").Id;
            spaghettiWithBrocoliSauce.Ingredients.Add(new Ingredient { ProductId = garlicId, Quantity = 3M, UnityType = UnityType.Unity });
            int brocoliId = productContext.Product.FirstOrDefault(p => p.Name == "Brocoli").Id;
            spaghettiWithBrocoliSauce.Ingredients.Add(new Ingredient { ProductId = brocoliId, Quantity = 15M, UnityType = UnityType.Kilogrammes });
            int spaghettiId = productContext.Product.FirstOrDefault(p => p.Name == "Spaghetti").Id;
            spaghettiWithBrocoliSauce.Ingredients.Add(new Ingredient { ProductId = spaghettiId, Quantity = 10M, UnityType = UnityType.Kilogrammes });

            return spaghettiWithBrocoliSauce;
        }

        private static Food MixedVegetableCurry(ProductContext productContext)
        {
            Food mixedVegetableCurry = new Food { Name = "Mixed vegetable curry", NumberOfPeople = 90 };
            mixedVegetableCurry.Ingredients = new List<Ingredient>();
            int onionId = productContext.Product.FirstOrDefault(p => p.Name == "Onion").Id;
            mixedVegetableCurry.Ingredients.Add(new Ingredient { ProductId = onionId, Quantity = 2M, UnityType = UnityType.Kilogrammes });
            int potatoesId = productContext.Product.FirstOrDefault(p => p.Name == "Potato").Id;
            mixedVegetableCurry.Ingredients.Add(new Ingredient { ProductId = potatoesId, Quantity = 10M, UnityType = UnityType.Kilogrammes });
            int cauliflowerId = productContext.Product.FirstOrDefault(p => p.Name == "Cauliflower").Id;
            mixedVegetableCurry.Ingredients.Add(new Ingredient { ProductId = cauliflowerId, Quantity = 5M, UnityType = UnityType.Kilogrammes });
            int zucchiniId = productContext.Product.FirstOrDefault(p => p.Name == "Zucchini").Id;
            mixedVegetableCurry.Ingredients.Add(new Ingredient { ProductId = zucchiniId, Quantity = 5M, UnityType = UnityType.Kilogrammes });
            int curryId = productContext.Product.FirstOrDefault(p => p.Name == "Curry").Id;
            mixedVegetableCurry.Ingredients.Add(new Ingredient { ProductId = curryId, Quantity = 0.050M, UnityType = UnityType.Kilogrammes });

            return mixedVegetableCurry;
        }

        private static Food CauliFlowerGratin(ProductContext productContext)
        {
            Food cauliFlowerGratin = new Food { Name = "Cauliflower gratin", NumberOfPeople = 90 };
            cauliFlowerGratin.Ingredients = new List<Ingredient>();
            int cauliflowerId = productContext.Product.FirstOrDefault(p => p.Name == "Cauliflower").Id;
            cauliFlowerGratin.Ingredients.Add(new Ingredient { ProductId = cauliflowerId, Quantity = 10M, UnityType = UnityType.Kilogrammes });
            int butterId = productContext.Product.FirstOrDefault(p => p.Name == "Butter").Id;
            cauliFlowerGratin.Ingredients.Add(new Ingredient { ProductId = butterId, Quantity = 1M, UnityType = UnityType.Kilogrammes });
            int flourId = productContext.Product.FirstOrDefault(p => p.Name == "Flour").Id;
            cauliFlowerGratin.Ingredients.Add(new Ingredient { ProductId = flourId, Quantity = 1M, UnityType = UnityType.Kilogrammes });
            int milkId = productContext.Product.FirstOrDefault(p => p.Name == "Milk").Id;
            cauliFlowerGratin.Ingredients.Add(new Ingredient { ProductId = milkId, Quantity = 10M, UnityType = UnityType.Liter });


            return cauliFlowerGratin;
        }

        private static Food SquashSoup(ProductContext productContext)
        {
            Food squashSoup = new Food { Name = "Squash soup", NumberOfPeople = 90 };
            squashSoup.Ingredients = new List<Ingredient>();
            int squashId = productContext.Product.FirstOrDefault(p => p.Name == "Squash").Id;
            squashSoup.Ingredients.Add(new Ingredient { ProductId = squashId, Quantity = 15M, UnityType = UnityType.Kilogrammes });
            int onionId = productContext.Product.FirstOrDefault(p => p.Name == "Onion").Id;
            squashSoup.Ingredients.Add(new Ingredient { ProductId = onionId, Quantity = 2M, UnityType = UnityType.Kilogrammes });
            int freshCreamId = productContext.Product.FirstOrDefault(p => p.Name == "Fresh Cream").Id;
            squashSoup.Ingredients.Add(new Ingredient { ProductId = freshCreamId, Quantity = 3M, UnityType = UnityType.Liter });

            return squashSoup;
        }

        private static Food BasicPicNic(ProductContext productContext)
        {
            Food basicPicnic = new Food { Name = "Basic Picnic", NumberOfPeople = 90 };
            basicPicnic.Ingredients = new List<Ingredient>();
            int breadId = productContext.Product.FirstOrDefault(p => p.Name == "Bread").Id;
            basicPicnic.Ingredients.Add(new Ingredient { ProductId = breadId, Quantity = 5M, UnityType = UnityType.Kilogrammes });
            int tomatoId = productContext.Product.FirstOrDefault(p => p.Name == "Tomato").Id;
            basicPicnic.Ingredients.Add(new Ingredient { ProductId = tomatoId, Quantity = 10M, UnityType = UnityType.Kilogrammes });
            int picnicCheeseId = productContext.Product.FirstOrDefault(p => p.Name == "Picnic Cheese").Id;
            basicPicnic.Ingredients.Add(new Ingredient { ProductId = picnicCheeseId, Quantity = 5M, UnityType = UnityType.Kilogrammes });
            int eggsId = productContext.Product.FirstOrDefault(p => p.Name == "Egg").Id;
            basicPicnic.Ingredients.Add(new Ingredient { ProductId = eggsId, Quantity = 90M, UnityType = UnityType.Unity });
            int lettuceId = productContext.Product.FirstOrDefault(p => p.Name == "Lettuce").Id;
            basicPicnic.Ingredients.Add(new Ingredient { ProductId = lettuceId, Quantity = 2M, UnityType = UnityType.Unity });


            return basicPicnic;
        }

        private static Food MafeSeitan(ProductContext productContext)
        {
            Food mafeSeitan = new Food { Name = "Seitan Mafé", NumberOfPeople = 90 };
            mafeSeitan.Ingredients = new List<Ingredient>();
            int onionId = productContext.Product.FirstOrDefault(p => p.Name == "Onion").Id;
            mafeSeitan.Ingredients.Add(new Ingredient { ProductId = onionId, Quantity = 2M, UnityType = UnityType.Kilogrammes });
            int glutenFlourId = productContext.Product.FirstOrDefault(p => p.Name == "Gluten flour").Id;
            mafeSeitan.Ingredients.Add(new Ingredient { ProductId = glutenFlourId, Quantity = 5M, UnityType = UnityType.Kilogrammes });
            int soySauceId = productContext.Product.FirstOrDefault(p => p.Name == "Soy sauce").Id;
            mafeSeitan.Ingredients.Add(new Ingredient { ProductId = soySauceId, Quantity = 0.5M, UnityType = UnityType.Liter });
            int peanutId = productContext.Product.FirstOrDefault(p => p.Name == "Peanut").Id;
            mafeSeitan.Ingredients.Add(new Ingredient { ProductId = peanutId, Quantity = 2M, UnityType = UnityType.Kilogrammes });
            int tomatoId = productContext.Product.FirstOrDefault(p => p.Name == "Tomato").Id;
            mafeSeitan.Ingredients.Add(new Ingredient { ProductId = tomatoId, Quantity = 5M, UnityType = UnityType.Kilogrammes });

            return mafeSeitan;
        }

        private static Food LimonPie(ProductContext productContext)
        {
            Food limonPie = new Food { Name = "Limon pie", NumberOfPeople = 90 };
            limonPie.Ingredients = new List<Ingredient>();
            int flourId = productContext.Product.FirstOrDefault(p => p.Name == "Flour").Id;
            limonPie.Ingredients.Add(new Ingredient { ProductId = flourId, Quantity = 2.5M, UnityType = UnityType.Kilogrammes });
            int butterId = productContext.Product.FirstOrDefault(p => p.Name == "Butter").Id;
            limonPie.Ingredients.Add(new Ingredient { ProductId = butterId, Quantity = 1.25M, UnityType = UnityType.Kilogrammes });
            int sugarId = productContext.Product.FirstOrDefault(p => p.Name == "Sugar").Id;
            limonPie.Ingredients.Add(new Ingredient { ProductId = sugarId, Quantity = 2M, UnityType = UnityType.Kilogrammes });
            int eggsId = productContext.Product.FirstOrDefault(p => p.Name == "Egg").Id;
            limonPie.Ingredients.Add(new Ingredient { ProductId = eggsId, Quantity = 50M, UnityType = UnityType.Unity });
            int limonId = productContext.Product.FirstOrDefault(p => p.Name == "Limon").Id;
            limonPie.Ingredients.Add(new Ingredient { ProductId = limonId, Quantity = 8M, UnityType = UnityType.Kilogrammes });

            return limonPie;
        }

        private static Food VegetarianLasagna(ProductContext productContext)
        {
            Food lasagna = new Food { Name = "Vegetarian Lasagna", NumberOfPeople = 90 };
            lasagna.Ingredients = new List<Ingredient>();
            int butterId = productContext.Product.FirstOrDefault(p => p.Name == "Butter").Id;
            lasagna.Ingredients.Add(new Ingredient { ProductId = butterId, Quantity = 1M, UnityType = UnityType.Kilogrammes });
            int flourId = productContext.Product.FirstOrDefault(p => p.Name == "Flour").Id;
            lasagna.Ingredients.Add(new Ingredient { ProductId = flourId, Quantity = 1M, UnityType = UnityType.Kilogrammes });
            int milkId = productContext.Product.FirstOrDefault(p => p.Name == "Milk").Id;
            lasagna.Ingredients.Add(new Ingredient { ProductId = milkId, Quantity = 10M, UnityType = UnityType.Liter });
            int tomatoId = productContext.Product.FirstOrDefault(p => p.Name == "Tomato").Id;
            lasagna.Ingredients.Add(new Ingredient { ProductId = tomatoId, Quantity = 10M, UnityType = UnityType.Kilogrammes });
            int onionId = productContext.Product.FirstOrDefault(p => p.Name == "Onion").Id;
            lasagna.Ingredients.Add(new Ingredient { ProductId = onionId, Quantity = 3M, UnityType = UnityType.Kilogrammes });
            int zucchiniId = productContext.Product.FirstOrDefault(p => p.Name == "Zucchini").Id;
            lasagna.Ingredients.Add(new Ingredient { ProductId = zucchiniId, Quantity = 5M, UnityType = UnityType.Kilogrammes });
            int noodleId = productContext.Product.FirstOrDefault(p => p.Name == "Lasagna Noodle").Id;
            lasagna.Ingredients.Add(new Ingredient { ProductId = noodleId, Quantity = 3M, UnityType = UnityType.Kilogrammes });

            return lasagna;
        }

        private static Food FruitSalad(ProductContext productContext)
        {
            Food fruitSalad = new Food { Name = "Fruit salad", NumberOfPeople = 90 };
            fruitSalad.Ingredients = new List<Ingredient>();
            int bananaId = productContext.Product.FirstOrDefault(p => p.Name == "Banana").Id;
            fruitSalad.Ingredients.Add(new Ingredient { ProductId = bananaId, Quantity = 45M, UnityType = UnityType.Unity });
            int appleId = productContext.Product.FirstOrDefault(p => p.Name == "Apple").Id;
            fruitSalad.Ingredients.Add(new Ingredient { ProductId = appleId, Quantity = 30M, UnityType = UnityType.Unity });
            int orangeId = productContext.Product.FirstOrDefault(p => p.Name == "Orange").Id;
            fruitSalad.Ingredients.Add(new Ingredient { ProductId = orangeId, Quantity = 10M, UnityType = UnityType.Unity });

            return fruitSalad;
        }
        #endregion

        #region utils methods
        private static async Task AddProductAndStockToDB(Product product, Stock stock, ProductContext context)
        {
            if (stock != null)
            {
                try
                {
                    if (product.Stocks == null)
                    {
                        product.Stocks = new List<Stock>();
                    }
                    product.Stocks.Add(stock);
                }
                catch (Exception ex)
                {

                }
            }
            context.Add(product);
            await context.SaveChangesAsync();
        }
        private static void AddSimpleFoodToMeal(string name, decimal quantity, UnityType unityType, Meal meal, ProductContext productContext)
        {
            Food simpleFood = new Food { Name = name, NumberOfPeople = 90 };
            simpleFood.Ingredients = new List<Ingredient>();
            simpleFood.Ingredients.Add(new Ingredient { Product = productContext.Product.FirstOrDefault(p => p.Name == name), Quantity = quantity, UnityType = unityType });
            meal.MealFoods.Add(new MealFood { Meal = meal, Food = simpleFood });
        }

        private static async Task AddGenMaiFood(ProductContext productContext)
        {
            Food genmai = new Food { Name = "genmai", NumberOfPeople = 100 };
            if (genmai.Ingredients == null)
            {
                genmai.Ingredients = new List<Ingredient>();
            }

            genmai.Ingredients.Add(new Ingredient { Product = productContext.Product.FirstOrDefault(p => p.Name == "Brown rice"), Quantity = 3M, UnityType = UnityType.Kilogrammes });
            genmai.Ingredients.Add(new Ingredient { Product = productContext.Product.FirstOrDefault(p => p.Name == "Onion"), Quantity = 1M, UnityType = UnityType.Kilogrammes });
            genmai.Ingredients.Add(new Ingredient { Product = productContext.Product.FirstOrDefault(p => p.Name == "Carot"), Quantity = 1M, UnityType = UnityType.Kilogrammes });
            genmai.Ingredients.Add(new Ingredient { Product = productContext.Product.FirstOrDefault(p => p.Name == "Leek"), Quantity = 1M, UnityType = UnityType.Kilogrammes });
            genmai.Ingredients.Add(new Ingredient { Product = productContext.Product.FirstOrDefault(p => p.Name == "Turnip"), Quantity = 1M, UnityType = UnityType.Kilogrammes });
            genmai.Ingredients.Add(new Ingredient { Product = productContext.Product.FirstOrDefault(p => p.Name == "Celery"), Quantity = 1M, UnityType = UnityType.Kilogrammes });

            productContext.Food.Add(genmai);
            try
            {
                await productContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
        }
        #endregion
    }
}
