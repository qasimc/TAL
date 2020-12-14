using System;
using System.Collections.Generic;
using System.Text;

namespace TAL.Domain.Models
{
    public class ErrorType
    {
        public readonly string Type;
        public readonly string Title;

        private ErrorType(string type, string title)
        {
            Type = type;
            Title = title;
        }
        public static readonly ErrorType InternalServerError = new ErrorType("InternalServerError", "There was an internal server error");
        public static readonly ErrorType OccupationNotFound = new ErrorType("OccupationNotFound", "Occupation not found in the list of occupations.");
        public static readonly ErrorType OccupationRatingNotFound = new ErrorType("OccupationRatingNotFound", "Occupation rating not found.");
        public static readonly ErrorType InvalidInput = new ErrorType("InvlidInput", "One or more fields were not entered in the correct format.");
    }
}
