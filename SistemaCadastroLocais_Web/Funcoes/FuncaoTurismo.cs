using Newtonsoft.Json;
using SistemaCadastroLocais_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCadastroLocais_Web.Funcoes
{
    public class FuncaoTurismo
    {
        //Recebe o endereço de hospedagem do Banco de Dados
        private string URLBackEnd = "https://localhost:44333";
        

        public List<TB_PontosTuristicos> getAll(string search)
        {

            var list = new List<TB_PontosTuristicos>();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(URLBackEnd);

                var responseTask = cliente.GetAsync("/api/PontoTuristico?search=");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    list = JsonConvert.DeserializeObject<List<TB_PontosTuristicos>>(readTask.Result);
                }

            }
            return list;
        }


        public List<TB_PontosTuristicos> getNome(string search)
        {

            var list = new List<TB_PontosTuristicos>();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(URLBackEnd);

                var responseTask = cliente.GetAsync($"/api/PontoTuristico?search={search}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    list = JsonConvert.DeserializeObject<List<TB_PontosTuristicos>>(readTask.Result);
                }

            }
            return list;
        }

        public bool Add(TB_PontosTuristicos model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(URLBackEnd);
                var Json = JsonConvert.SerializeObject(model);
                var data = new StringContent(Json, Encoding.UTF8, "application/json");

                var responseTask = cliente.PostAsync("/api/PontoTuristico", data);

                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return true;
                }

            }
            return false;
        }
        public TB_PontosTuristicos findById(int? id)
        {

            var ponto = new TB_PontosTuristicos();
            using (var LocaisId = new HttpClient())
            {
                LocaisId.BaseAddress = new Uri(URLBackEnd);

                var responseTask = LocaisId.GetAsync($"/api/PontoTuristico/{id.Value}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var retorno = JsonConvert.DeserializeObject<TB_PontosTuristicos>(readTask.Result);

                    if (retorno == null)
                        return null;
                    ponto = retorno;
                }
                else
                {
                    return null;
                }

            }
            return ponto;
        }

        public bool Edit(TB_PontosTuristicos model)
        {

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(URLBackEnd);

                var json = JsonConvert.SerializeObject(model);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var responseTask = cliente.PutAsync($"/api/PontoTuristico/{model.LocaisId}", data);

                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return true;

                }

            }

            return false;

        }

        public bool Delete(int? id)
        {

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(URLBackEnd);

                var responseTask = cliente.DeleteAsync($"/api/PontoTuristico/{id.Value}");

                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;

                }

            }

            return false;

        }

    }


}
