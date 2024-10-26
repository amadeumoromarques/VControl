using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData;
using Newtonsoft.Json.Serialization;
using System.Collections.ObjectModel;
using System.Text;
using Newtonsoft.Json;
using System.Reflection;
using System.Resources;
using Microsoft.AspNet.OData.Extensions;

namespace AI.Infra
{
    public class ErrorReturnODataOutputFormatter : ODataOutputFormatter
    {
        private readonly JsonSerializer serializer;

        private readonly bool showDetailedError;

        public ErrorReturnODataOutputFormatter(bool showDetailedError)
            : base(new ODataPayloadKind[1] { ODataPayloadKind.Error })
        {
            serializer = new JsonSerializer
            {
                ContractResolver = new DefaultContractResolver()
            };
            this.showDetailedError = showDetailedError;
            ((Collection<string>)base.SupportedMediaTypes).Add("application/json");
            base.SupportedEncodings.Add(new UTF8Encoding());
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            SerializableError serializableError = context.Object as SerializableError;
            if (serializableError == null)
            {
                return base.WriteResponseBodyAsync(context, selectedEncoding);
            }

            using StreamWriter streamWriter = new StreamWriter(context.HttpContext.Response.Body);
            serializer.Serialize(streamWriter, TransformToErrorReturn(serializableError, showDetailedError));
            return streamWriter.FlushAsync();
        }

        private static ErrorReturn TransformToErrorReturn(SerializableError serializableError, bool showDetailedError)
        {
            ODataError oDataError = serializableError.CreateODataError();
            ErrorReturn errorReturn = null;
            if (showDetailedError)
            {
                errorReturn = new ErrorReturn(oDataError.Message);
                ODataInnerError innerError = oDataError.InnerError;
                int num = 0;
                while (innerError != null)
                {
                    string text = ((num == 0) ? string.Empty : num.ToString());
                    errorReturn.ValidationFields.Add(text + "Message", new List<string> { innerError.Message });
                    errorReturn.ValidationFields.Add(text + "StackTrace", new List<string> { innerError.StackTrace });
                    errorReturn.ValidationFields.Add(text + "TypeName", new List<string> { innerError.TypeName });
                    innerError = innerError.InnerError;
                    num++;
                }
            }
            else
            {
                errorReturn = new ErrorReturn("An error has occured");
                errorReturn.ValidationFields.Add("Message", new List<string> { oDataError.Message });
            }

            return errorReturn;
        }
    }

    public class ErrorReturn
    {
        public const string DefaultErrorMessage = "An error has occured";

        public ValidationInfo Message { get; set; }

        public Dictionary<string, List<string>> ValidationFields { get; set; }

        public ErrorReturn(string message)
        {
            Message = new ValidationInfo(message);
            ValidationFields = new Dictionary<string, List<string>>();
        }

        public ErrorReturn(Exception exception)
        {
            if (exception is IntegraException)
            {
                IntegraException ex = exception as IntegraException;
                Message = ex.ValidationInfoMessage;
                ValidationFields = ex.Fields;
            }
            else
            {
                Message = new ValidationInfo(exception.Message);
                ValidationFields = new Dictionary<string, List<string>>();
            }
        }
    }

    public class ValidationInfo
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public ValidationInfo(string message)
        {
            string[] array = message.Split('|');
            if (array.Length > 1)
            {
                Id = array.First();
                Message = array.Last();
            }
            else
            {
                Message = message;
            }
        }

        public ValidationInfo(string codigo, string mensagem)
        {
            Id = codigo;
            Message = mensagem;
        }

        public static ValidationInfo GetMensagemErro(Type t, string codigo)
        {
            Assembly assembly = t.Assembly;
            string @string = new ResourceManager(t.Assembly.GetName().Name + ".Properties." + t.Name + "Resources", assembly).GetString(codigo);
            if (string.IsNullOrEmpty(@string))
            {
                string message = $"Código de erro '{codigo}' não implementado no arquivo de resources";
                throw new ArgumentNullException(codigo, message);
            }

            return new ValidationInfo(codigo, @string);
        }

        public static ValidationInfo GetMensagemErro(Type t, string codigo, params object[] args)
        {
            ValidationInfo mensagemErro = GetMensagemErro(t, codigo);
            mensagemErro.Message = string.Format(mensagemErro.Message, args);
            return mensagemErro;
        }

        public static ValidationInfo GetInternalServerErrorValidationInfo()
        {
            return new ValidationInfo("INTERNAL0001", "Ocorreu um erro interno no servidor");
        }
    }

    public class IntegraException : Exception
    {
        public ValidationInfo ValidationInfoMessage { get; private set; }

        public Dictionary<string, List<string>> Fields { get; set; }

        public IntegraException(ValidationInfo validationInfoMessage)
            : this(validationInfoMessage.Id.ToString(), null, validationInfoMessage)
        {
        }

        public IntegraException(ValidationInfo validationInfoMessage, Exception innerException)
            : this(validationInfoMessage.Id.ToString(), innerException, validationInfoMessage)
        {
        }

        public IntegraException(string message, ValidationInfo validationInfoMessage)
            : this(message, null, validationInfoMessage)
        {
        }

        public IntegraException(string message, Exception innerException, ValidationInfo validationInfoMessage)
            : base(message, innerException)
        {
            ValidationInfoMessage = validationInfoMessage;
            Fields = new Dictionary<string, List<string>>();
        }
    }
}