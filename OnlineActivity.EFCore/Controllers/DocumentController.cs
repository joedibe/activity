using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OnlineActivity.EFCore.Domain;
using OnlineActivity.EFCore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.EFCore.WebApi.Controllers
{
    [EnableCors("OnlineActivityAngular6")]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private IDocumentRepository documentRepo;

        public DocumentController(IDocumentRepository documentRepo)
        {
            this.documentRepo = documentRepo;
        }
        // GET: api/Document
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Document>))]
        public ActionResult<IEnumerable<Document>> Get()
        {
            return Ok(documentRepo.Retrieve().ToList());
        }

        // GET: api/Document/5
        [HttpGet("{id}", Name = "GetDocumentByID")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Document))]
        public async Task<ActionResult<Document>> Get(Guid id)
        {
            try
            {
                var result = await documentRepo.RetrieveAsync(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST: api/Document
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201, Type = typeof(Document))]
        public async Task<ActionResult<Document>> Post([FromBody] Document document)
        {
            try
            {
                document.DocumentID = Guid.NewGuid();
                await documentRepo.CreateAsync(document);
                return CreatedAtRoute("GetDocumentByID",
                    new
                    {
                        id = document.DocumentID
                    },
                    document);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT: api/Document/5
        [HttpPut("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Document))]
        public async Task<ActionResult<Document>> Put(Guid id, [FromBody] Document document)
        {
            try
            {
                var result = documentRepo.Retrieve().FirstOrDefault(x => x.DocumentID == id);
                if (result == null)
                {
                    return NotFound();
                }
                await documentRepo.UpdateAsync(id, document);

                return Ok(document);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var result = documentRepo.Retrieve().FirstOrDefault(x => x.DocumentID == id);
                if (result == null)
                {
                    return NotFound();
                }

                await documentRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
