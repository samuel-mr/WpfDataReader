using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using WebDataReader.Domain;
using WebDataReader.Domain.ValueObjects.Transform;
using Xunit;

namespace WebDataReader.DomainTest
{
    public class TransformTemplateTest
    {
        [Fact]
        public void WhenIsUpperCaseWithUpperCaseAlready_ShouldLeaveItAsIs()
        {
            string template = "{{Name}}";
            var result = new TransformTemplate().TransformLine(template, new ColumnMetadata() { ColumnName = "Inminuscula", DataType = "String" });
            result.Should().Be(@"Inminuscula");
        }
        [Fact]
        public void WhenIsUpperCase_ShouldTransformFirstCharacter()
        {
            string template = "{{Name}}";
            var result = new TransformTemplate().TransformLine(template, new ColumnMetadata() { ColumnName = "inminuscula", DataType = "String" });
            result.Should().Be(@"Inminuscula");
        }

        [Fact]
        public void TransfornLine_SimpleTest()
        {
            string template = "public {{type}} {{name}} { get; set; }";
            var result = new TransformTemplate().TransformLine(template, new ColumnMetadata() { ColumnName = "Nombre", DataType = "String" });
            result.Should().Be(@"public string Nombre { get; set; }");
        }

        [Fact]
        public void Nullable_String_TransfornLine_SimpleTest()
        {
            string template = "public {{type}} {{name}} { get; set; }";
            var result = new TransformTemplate().TransformLine(template, new ColumnMetadata() { ColumnName = "Nombre", DataType = "String", AllowDbNull = true });
            result.Should().Be(@"public string Nombre { get; set; }");
        }
        [Fact]
        public void Nullable_int_TransfornLine_SimpleTest()
        {
            string template = "public {{type}} {{name}} { get; set; }";
            var result = new TransformTemplate().TransformLine(template, new ColumnMetadata() { ColumnName = "Nombre", DataType = "int", AllowDbNull = true });
            result.Should().Be(@"public int? Nombre { get; set; }");
        }

        [Fact]
        public void TransfornMultiple_SimpleTest()
        {
            string template = "public {{type}} {{name}} { get; set; }";
            var result = new TransformTemplate().Transform(template, new List<ColumnMetadata>() {
        new ColumnMetadata() { ColumnName  = "Nombre", DataType =  "string"},
        new ColumnMetadata() { ColumnName = "Cantidad", DataType =  "int"}
        }
            );
            result.Should().Be(@"public string Nombre { get; set; }
public int Cantidad { get; set; }
");
        }

        [Fact]
        public void IsRequired_Demo1_Test()
        {
            string template = "{{IsRequired?hola::mundo}}";
            var result = new TransformTemplate().ExtractSingle_Is(template, new ColumnMetadata()
            {
                ColumnName = "Nombre",
                DataType = "string",
                AllowDbNull = false
            });

            result.Should().Be(@"hola");
        }
        [Fact]
        public void IsNotRequired_Demo1_Test()
        {
            string template = "{{IsRequired?hola::mundo}}";
            var result = new TransformTemplate().ExtractSingle_Is(template, new ColumnMetadata()
            {
                ColumnName = "Nombre",
                DataType = "string",
                AllowDbNull = true
            });

            result.Should().Be(@"mundo");
        }
    }
}
