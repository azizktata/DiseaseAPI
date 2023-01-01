using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using DataAcess;
using Domain.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace diseaseAPI_DotNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;

        public ArticleController (IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: api/<ArticleController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Article>> Get()
        {
            return Ok(unitOfWork.Article.GetAll());
        }

        
        // GET api/<ArticleController>/diseaseName
        [HttpGet("{diseaseId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Article>> GetByDiseaseId(int diseaseId)
        {
            var Articles = unitOfWork.Article.GetAll();
            List<Article> Selected = new List<Article>();
            if(diseaseId<=0)
            {
                return BadRequest();
            }
            foreach(Article article in Articles)
            {
                if (article.diseaseId == diseaseId)
                    Selected.Add(article);
                    
            }

            /*if (Selected.Count == 0)
            {
                return NotFound("Article not found");
            }*/
            
            return Ok(Selected);
        }

        // POST api/<ArticleController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Article>> AddArticle(AddArticleDto request)
        {
            
            if(request.diseaseId <=0)
            {
                return BadRequest("invalid request");
            }
            
            var Article = new Article
            {
                title = request.title,
                description = request.description,
                url = request.url,
                diseaseId = request.diseaseId


            };
            

            unitOfWork.Article.Add(Article);
            unitOfWork.Save();
            var disease =  unitOfWork.Disease.GetById(Article.Id);
            if (disease == null)
                return NotFound("disease not found");


            disease.articles.Add(Article);
            unitOfWork.Disease.Update(disease);
            unitOfWork.Save();
            return Ok(disease);   
        }

        // PUT api/<ArticleController>/5
       /* [HttpPut("{id}")]
        public async Task<ActionResult<List<Article>>> UpdateArticle(int id, Article Article)
        {
            var Articlenw = await _context.Articles.FindAsync(id);
            if (Articlenw == null)
                return BadRequest("Article not found");
            Articlenw.firstName = Article.firstName;
            Articlenw.lastName = Article.lastName;
            Articlenw.email = Article.email;
            Articlenw.location= Article.location;
            Articlenw.password = Article.password;
            _context.Articles.Update(Articlenw);
            await _context.SaveChangesAsync();
            return Ok(Articlenw);
        }
       */
        // DELETE api/<ArticleController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Article>>> DeleteArticle(int id)
        {
            if (id <= 0)
                return BadRequest("invalid input");
            var article = unitOfWork.Article.GetById(id);
            if (article == null)
                return NotFound();
            unitOfWork.Article.Remove(article);
            unitOfWork.Save();
            return Ok();
        }
    }
}
