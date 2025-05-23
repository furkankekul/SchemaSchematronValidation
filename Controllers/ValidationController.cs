﻿using C_BitirmeOdevi.ValidationBase;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace C_BitirmeOdevi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        [HttpPost("ValidationControl")]
        public IActionResult ValidationControl(IFormFile file, DocumentType documentType, Validation validationType)
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
                    if (schemaResponse.Key)
                        return Ok(schemaResponse.Value);
                    return BadRequest(schemaResponse.Value);
                case Validation.Schematron:
                    schematronResponse = validator.SchematronControl(xmlString);
                    if (schematronResponse.Key)
                        return Ok(schematronResponse);
                    return BadRequest(schematronResponse);
                case Validation.All:
                    schemaResponse = validator.SchemaControl(xmlString, documentType);
                    if (!schemaResponse.Key)
                        return BadRequest(schemaResponse.Value);
                    schematronResponse = validator.SchematronControl(xmlString);
                    if (schematronResponse.Key)
                        return Ok(schematronResponse.Value);
                    return BadRequest(false);
                default:
                    break;
            }

            return BadRequest("Validasyon türünü doğru giriniz.");

        }
    }
}
