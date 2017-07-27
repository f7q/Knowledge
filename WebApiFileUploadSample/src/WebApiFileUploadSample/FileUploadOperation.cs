using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.Swagger.Model;
using Swashbuckle.SwaggerGen.Generator;


namespace WebApiFileUploadSample
{
    public class FileUploadOperation : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.OperationId.ToLower() == "apivaluesuploadpost" ||
                operation.OperationId.ToLower() == "apifilesuploadpost")
            {
                operation.Parameters.Clear();
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "uploadedFile",
                    In = "formData",
                    Description = "Upload File",
                    Required = true,
                    Type = "file"
                });
                operation.Consumes.Add("multipart/form-data");
            }

            // オペレーションIDはSwaggerから調べるBy{id}
            if (operation.OperationId.ToLower() == "apiuploaduploadbyidpost")
            {
                var param = operation.Parameters[0]; // パラメータ取得By{id}部分
                operation.Parameters.Clear();
                operation.Parameters.Add(param);
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "uploadedFile",
                    In = "formData",
                    Description = "Upload File",
                    Required = true,
                    Type = "file"
                });
                operation.Consumes.Add("multipart/form-data");
            }
        }

    }
}
