using diseaseAPI_DotNet6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace diseaseAPI_DotNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        private readonly diseaseDbContext _context;

        public ArticleController (diseaseDbContext context)
        {
            _context = context;
        }
        // GET: api/<ArticleController>
        [HttpGet]
        public async Task<ActionResult<List<Article>>> Get()
        {
            return Ok(await _context.Articles.ToListAsync());
        }

        
        // GET api/<ArticleController>/diseaseName
        [HttpGet("{diseaseId:int}")]
        public async Task<ActionResult<List<Article>>> GetByDiseaseName(int diseaseId)
        {
            List<Article> Articles = await _context.Articles.ToListAsync();
            List<Article> Selected = new List<Article>();
            foreach(Article article in Articles)
            {
                if (article.diseaseId == diseaseId)
                    Selected.Add(article);
                    
            }
            if (Selected.Count == 0)
            {
                return BadRequest("Article not found");
            }
            return Ok(Selected);
        }

        // POST api/<ArticleController>
        [HttpPost]
        public async Task<ActionResult<List<Article>>> AddArticle(Article Article)
        {
            _context.Articles.Add(Article);
            _context.SaveChanges();
            var disease =  await _context.Diseases.FindAsync(Article.diseaseId);
            if (disease == null)
                return BadRequest("disease not found");
            //var articles = new List<Article>();
            //articles.Add(Article);
            disease.articles.Add(Article);
            

            //articles.ToList<Article>().ForEach(tchr => _context.Entry(tchr).State = EntityState.Added);
            //_context.Entry(disease.articles).State = EntityState.Added;
            //var entry = _context.Entry(disease);
            

            //disease.articles.ForEach(item => _context.Entry(item).State = EntityState.Modified);
            foreach(Article item in disease.articles)
            {
              
                _context.Entry(item).State = EntityState.Modified;
             

            }
            _context.Diseases.Entry(disease).State = EntityState.Added;
            _context.Diseases.Update(disease);
            _context.SaveChanges();
            
            await _context.SaveChangesAsync();  
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
            _context.Articles.Remove(_context.Articles.Find(id));   
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
