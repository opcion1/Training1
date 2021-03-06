﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Training1.Models;
using Training1.Repositories;
using Training1.Services.Interfaces;

namespace Training1.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Produces("application/json")]  
        public async Task<IActionResult> Create([Bind("FoodId,ProductId,Quantity,UnityType")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await _ingredientService.CreateAsync(ingredient);
            }
            //We want to return ingredient with the product name
            ingredient = await _ingredientService.GetByIdAsync(ingredient.Id);
            return Ok(ingredient);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Produces("application/json")]
        public async Task<IActionResult> Edit([Bind("Id,FoodId,ProductId,Quantity,UnityType")] Ingredient ingredient)
        {
            try
            {
                await _ingredientService.EditAsync(ingredient);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await IngredientExists(ingredient.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            ingredient = await _ingredientService.GetByIdAsync(ingredient.Id);
            return Ok(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ingredientService.DeleteAsync(id);
            return Ok();
        }


        // GET: Ingredients/Details/5
        [Produces("application/json")]
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var ingredient = await _ingredientService.GetByIdAsync((int)id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return Ok(ingredient);
        }

        private async Task<bool> IngredientExists(int id)
        {
            return await _ingredientService.ExistsEntity(id);
        }
    }
}
