using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Domain.UniqueMessages
{
     public class UniqueMessage
    {
        //NOTE: string and datetime works out of the box with JSON serialization - but other types may not (e.g decimal does not - its gets deserialized to double)
        //If you need other types checkout https://codeoverload.wordpress.com/2013/12/11/deserialize-derived-objects-in-json-net/
        //PS. do NOT add all kinds of high level BIO objects here - only base types that needs to be localized.
        private static readonly List<Type> AllowedTypes = new List<Type> { typeof(string), typeof(DateTime)}; 

        public UniqueMessageTemplate Template { get; set; }
        public UniqueMessageType MessageType { get; set; }

        /// <summary>
        /// List of messages parameters
        /// </summary>
        public object[] Params { get; set; }

        public UniqueMessage(UniqueMessageTemplate uniqueMessageTemplate, UniqueMessageType messageType = UniqueMessageType.UserInfo, object[] parameters = null)
        {
            ValidateParameters(parameters);

            Template = uniqueMessageTemplate;
            MessageType = messageType;
            Params = parameters;
        }

        private static void ValidateParameters(IEnumerable parameters)
        {
            if (parameters == null)
                return;

            foreach (var parameter in parameters)
            {
                var type = parameter.GetType();
                if (!AllowedTypes.Contains(type))
                {
                    //throw new ArgumentException($"Type {type} not supported");
                    throw new ArgumentException(string.Format("Type {0} not supported", type));
                }
            }
        }

        public string GetFormatedText()
        {
            if (Params == null)
            {
                return Template.Text;
            }
            return string.Format(Template.Text, Params);
        }

        public static UniqueMessage CreateValidationErrorFromTemplate(UniqueMessageTemplate uniqueMessageTemplate, object[] parameters = null)
        {
            return new UniqueMessage(uniqueMessageTemplate, UniqueMessageType.ValidationError, parameters);
        }

        public static UniqueMessage CreateSystemErrorFromTemplate(UniqueMessageTemplate uniqueMessageTemplate, object[] parameters = null)
        {
            return new UniqueMessage(uniqueMessageTemplate, UniqueMessageType.SystemError, parameters);
        }

        public static UniqueMessage CreateUserInfoFromTemplate(UniqueMessageTemplate uniqueMessageTemplate, object[] parameters = null)
        {
            return new UniqueMessage(uniqueMessageTemplate, UniqueMessageType.UserInfo, parameters);
        }

        public static UniqueMessage CreateValidationErrorFromTemplate(UniqueMessageTemplate uniqueMessageTemplate, object parameter)
        {
            return CreateValidationErrorFromTemplate(uniqueMessageTemplate, new[] {parameter});
        }

        public static UniqueMessage CreateSystemErrorFromTemplate(UniqueMessageTemplate uniqueMessageTemplate, object parameter)
        {
            return CreateSystemErrorFromTemplate(uniqueMessageTemplate, new[] {parameter});
        }

        public static UniqueMessage CreateUserInfoFromTemplate(UniqueMessageTemplate uniqueMessageTemplate, object parameter)
        {
            return CreateUserInfoFromTemplate(uniqueMessageTemplate, new[] {parameter});
        }


        public override bool Equals(Object obj)
        {
            // If parameter cannot be cast to UniqueMessage return false.
            var uniqueMessage = obj as UniqueMessage;
            if (uniqueMessage == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Template.Id == uniqueMessage.Template.Id);
        }

        public bool Equals(UniqueMessage uniqueMessage)
        {
            // If parameter is null return false:
            if ((object)uniqueMessage == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Template.Id == uniqueMessage.Template.Id);
        }

        public override int GetHashCode()
        {
            return Template.Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetFormatedText();
        }
    }
}
