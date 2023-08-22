using InnoTask.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ThirdApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly string _allocationPath = "App_Data\\Forms";

        public FormsController()
        {
            EnsureDirectoryExists();
        }

        private void EnsureDirectoryExists()
        {
            if (!Directory.Exists(_allocationPath))
            {
                Directory.CreateDirectory(_allocationPath);
            }
        }

        private string GetFilePathForType(string type)
        {
            return Path.Combine(_allocationPath, $"{type}.json");
        }

        [HttpPost]
        public ActionResult CreateForm([FromBody] Form Form)
        {
            if (Form is null) throw new ArgumentNullException(nameof(Form));

            StoreFormInFile(Form);

            return Ok();
        }

        private void StoreFormInFile(Form Form)
        {
            var filePath = GetFilePathForType(Form.Type);
            var FormsOfType = ReadFormsFromFile(filePath);

            FormsOfType.Add(Form);
            WriteFormsToFile(filePath, FormsOfType);
        }

        private List<Form> ReadFormsFromFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                string fileContent = System.IO.File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(fileContent))
                {
                    return JsonSerializer.Deserialize<List<Form>>(fileContent);
                }
            }

            return new List<Form>();
        }

        private void WriteFormsToFile(string filePath, List<Form> Forms)
        {
            var FormData = JsonSerializer.Serialize(Forms, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(filePath, FormData);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Form>> ReadAllForms()
        {
            var allForms = new List<Form>();
            var FormFiles = Directory.GetFiles(_allocationPath, "*.json");

            foreach (var filePath in FormFiles)
            {
                allForms.AddRange(ReadFormsFromFile(filePath));
            }

            return Ok(allForms);
        }

        [HttpGet("{type}")]
        public ActionResult<IEnumerable<Form>> ReadFormsByType(string type)
        {
            var filePath = GetFilePathForType(type);
            var FormsOfType = ReadFormsFromFile(filePath);

            return Ok(FormsOfType);
        }
    }
}
