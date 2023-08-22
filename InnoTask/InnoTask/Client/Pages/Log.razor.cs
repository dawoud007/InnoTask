/*using InnoTask.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace ThirdApp.Client
{
    public partial class Forms
    {


        private List<Form> StudentForms { get; set; } = new List<Form>();
        private Form Form { get; set; } = new Form();
        private string SelectedFilterType { get; set; } = "";

        private List<Form> FilteredForms
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SelectedFilterType))
                    return StudentForms;

                return StudentForms.Where(form => form.Type == SelectedFilterType).ToList();
            }
        }

        protected async Task OnFormSubmit()
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Forms", Form);
            if (response.IsSuccessStatusCode)
            {
                StudentForms.Add(Form);
                Form = new Form();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            StudentForms = await _httpClient.GetFromJsonAsync<List<Form>>("api/Forms");
        }
    }
}
*/