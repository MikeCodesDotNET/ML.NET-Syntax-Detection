// This file was auto-generated by ML.NET Model Builder. 

using Microsoft.ML.Data;

namespace LanguageDetectionMLML.Model
{
    public class ModelInput
    {
        [ColumnName("Language"), LoadColumn(0)]
        public string Language { get; set; }


        [ColumnName("Content"), LoadColumn(1)]
        public string Content { get; set; }


    }
}
