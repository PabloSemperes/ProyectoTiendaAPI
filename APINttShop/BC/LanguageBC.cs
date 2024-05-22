using API_nttshop.DAC;
using API_nttshop.Models;
using API_nttshop.Models.Reponse.LanguageResponse;
using APINttShop.Models.Request;
using APINttShop.Models.Response.LanguageResponse;

namespace API_nttshop.BC
{
    public class LanguageBC
    {
        private readonly LanguageDAC languageDAC = new LanguageDAC();
        //Mi primer comentario en desarrollo
        public GetAllLanguagesResponse getAllLanguages()
        {
            GetAllLanguagesResponse result = new GetAllLanguagesResponse();

            result.languageList = languageDAC.GetAllLanguages();

            if (result.languageList.Count() > 0)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.NoContent;
                result.message = "No content";
            }

            return result;
        }
        //Agarrar solo un idioma
        public GetLanguageResponse getLanguage(IdRequest request)
        {
            GetLanguageResponse result = new GetLanguageResponse();

            if (GetLanguageValidation(request))
            {
                result.language = languageDAC.GetLanguages(request.id);

                if (result.language != null) 
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.NoContent;
                    result.message = "No content";
                }
            }

            return result;
        }

        public BaseReponseModel InsertLanguage(LanguageRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (InsertLanguageValidation(request))
            {
                bool correctOperation = languageDAC.InsertLanguage(request.language);

                if (correctOperation)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.Conflict;
                    result.message = "That ISO already exists.";
                }
            }
            else 
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }

            return result;
        }

        public BaseReponseModel UpdateLanguage(LanguageRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (UpdateLanguageValidation(request))
            {
                byte correctOperation = languageDAC.UpdateLanguage(request.language);

                if (correctOperation == 1) result.httpStatus = System.Net.HttpStatusCode.OK;
                else if(correctOperation == 0) result.httpStatus = System.Net.HttpStatusCode.NotFound;
                else if (correctOperation == 2) result.httpStatus = System.Net.HttpStatusCode.Conflict;
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }


            return result;
        }

        public BaseReponseModel DeleteLanguage(IdRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (DeleteLanguageValidation(request))
            {
                bool correctOperation = languageDAC.DeleteLanguage(request.id);

                if (correctOperation)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.Conflict;
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }


            return result;
        }

        private bool GetLanguageValidation(IdRequest request)
        {
            if (request != null
                && request.id > 0)
            {
                return true;
            }
            else return false;
        }

        private bool InsertLanguageValidation(LanguageRequest request) 
        {
            if (request != null
                && request.language != null
                && !string.IsNullOrWhiteSpace(request.language.description)
                && !string.IsNullOrWhiteSpace(request.language.iso)
                && request.language.iso.All(Char.IsLetter)
                && request.language.idLanguage > 0
               )
            {
                return true;
            }
            else return false;
        }

        private bool UpdateLanguageValidation(LanguageRequest request)
        {
            if (request != null
                && request.language != null
                && !string.IsNullOrWhiteSpace(request.language.description)
                && !string.IsNullOrWhiteSpace(request.language.iso)
                && request.language.iso.All(Char.IsLetter)
                && request.language.idLanguage > 0
               )
            {
                return true;
            }
            else return false;
        }

        private bool DeleteLanguageValidation(IdRequest request)
        {
            if (request != null
                && request.id > 0)
            {
                return true;
            }
            else return false;
        }
    }
}
