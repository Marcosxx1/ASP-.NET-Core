﻿using System.ComponentModel.DataAnnotations;

namespace ModelBindingAndValidations.model
{
    public class Book
    {

        public int? BookId { get; set; }
        public string? Author { get; set; }

        public override string ToString()
        {
            return $"Book Object - Book id: {BookId}, Book Author: {Author}";
        }
    }
}
