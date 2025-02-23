using C_BitirmeOdevi.ValidationBase;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace C_BitirmeOdevi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        [HttpPost("SchemaValidation")]
        public IActionResult SchemaValidation(IFormFile file, DocumentType documentType, Validation validationType)
        {
            string xmlString;
            KeyValuePair<bool, List<string>> schemaResponse = new KeyValuePair<bool, List<string>>();
            KeyValuePair<bool, string> schematronResponse = new KeyValuePair<bool, string>();
            Validator validator = new Validator();
            using (var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
            {
                xmlString = reader.ReadToEnd();
            }

            switch (validationType)
            {
                case Validation.Schema:
                    schemaResponse = validator.SchemaControl(xmlString, documentType);
                    break;
                case Validation.Schematron:
                    schematronResponse = validator.SchematronControl(xmlString);
                    if (schematronResponse.Key)
                        return Ok(schematronResponse);
                    return BadRequest(schematronResponse);
                case Validation.All:
                    schemaResponse = validator.SchemaControl(xmlString, documentType);
                    schematronResponse = validator.SchematronControl(xmlString);
                    if (schemaResponse.Key && schematronResponse.Key)
                        return Ok(true);
                    return BadRequest(false);
                default:
                    break;
            }

            return BadRequest();

        }
    }
}
