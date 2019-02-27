using System;
using BuffetDesigner.Domain._Base;
using BuffetDesigner.Domain.Enums;
using BuffetDesigner.Domain.Product.Dtos;

namespace BuffetDesigner.Domain.Product
{
    public class ProductStorer
    {
        private readonly IProductRepository _productRepository;

        public ProductStorer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Storer(ProductDto productDto)
        {
            var productFound = _productRepository.GetByCodBonanza(productDto.CodBonanza);

            RuleValidator.New()
                .When(productFound != null && productFound.Id != productDto.Id, Resource.ProductCodBonanzaAlreadyExists)
                .When(!Enum.TryParse<Status>(productDto.Status, out var status), Resource.InvalidStatus)
                .ThrowExceptionIfExists();

            if (productDto.Id == 0)
            {
                var product = new Product(productDto.NomeApresentacao, productDto.Descricao, productDto.CodBonanza, status, productDto.FotoProjeto,
                                            productDto.FotoIlustrativa, productDto.FotoDetalhe1, productDto.FotoDetalhe2, productDto.FotoDetalhe3);
                
                _productRepository.Add(product);
            }
            else
            {
                productFound = _productRepository.GetById(productDto.Id);

                RuleValidator.New()
                    .When(productFound == null, Resource.ProductNotFound)
                    .ThrowExceptionIfExists();

                productFound.ChangeCodBonanza(productDto.CodBonanza);
                productFound.ChangeNomeApresentacao(productDto.NomeApresentacao);
                productFound.ChangeFotoProjeto(productDto.FotoProjeto);
                productFound.ChangeStatus(status);
                productFound.ChangeDetails(productDto.Descricao, productDto.FotoIlustrativa, productDto.FotoDetalhe1,
                                             productDto.FotoDetalhe2, productDto.FotoDetalhe3);
            }
        }
    }
}