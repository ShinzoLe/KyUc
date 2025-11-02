using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Ingredient
{
    public string nameIngre;
    public int idIngre;
    public int ingerAmt;
}

[CreateAssetMenu(fileName ="Recipe", menuName ="Recipes/Recipe Object")]
public class RecipeScript : ScriptableObject
{
    public int idRecipe;
    public Ingredient[] ingredients;
}
