using NUnit.Framework;

namespace RecipeBook.Tests
{
    [TestFixture]
    public class RecipeTests
    {
        [Test]
        public void CalculateTotalCalories_ShouldReturnCorrectTotalCalories()
        {
            // Arrange
            Recipe recipe = new Recipe();
            recipe.Ingredients.Add(new Ingredient { Calories = 100 });
            recipe.Ingredients.Add(new Ingredient { Calories = 150 });
            recipe.Ingredients.Add(new Ingredient { Calories = 75 });

            // Act
            int totalCalories = recipe.CalculateTotalCalories();

            // Assert
            Assert.AreEqual(325, totalCalories);
        }
    }
}