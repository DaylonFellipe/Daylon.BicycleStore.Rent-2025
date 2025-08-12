

# 🚲 BikeRental - Sistema de Aluguel de Bicicletas

Sistema desenvolvido em **.NET** com foco em **boas práticas de código, arquitetura limpa e organização**, voltado para gerenciar o aluguel de bicicletas de forma simples.

---

## 📖 Sobre o Projeto
O **BicycleStore.Rent** é uma aplicação para gerenciamento de aluguel de bicicletas, construída com **Clean Architecture** e seguindo princípios de **SOLID**, **DDD (Domain-Driven Design)** e **Clean Code**.

O objetivo principal é fornecer um sistema **flexível**, **testável** e **fácil de manter**, aplicando padrões arquiteturais que permitam evolução contínua.

---

## 🏛 Arquitetura e Boas Práticas

- **Clean Architecture**: Separação clara de responsabilidades entre camadas.
- **DDD (Domain-Driven Design)**: Modelagem orientada ao domínio e linguagem ubíqua.
- **SOLID Principles**
- **Clean Code**: Código legível, padronizado e com nomenclaturas claras.
- **CQRS**: Separação de comandos (alterações de estado) e consultas (queries).
- **Injeção de Dependência**: Uso de DI para baixo acoplamento.
- **Documentação via Swagger**: Para facilitar o consumo da API.

---

## 🛠 Tecnologias Utilizadas

- **C# 12.0** Linguagem principal de desenvolvimento.
- **.NET 8**
- **Entity Framework Core** ORM para acesso e manipulação de dados no banco de dados relacional.
- **SQL Server**
- **System.Text.Json** Para serialização/deserialização de objetos JSON, incluindo suporte para enums com JsonStringEnumConverter.
- **FluentValidation** (validações)
- **AutoMapper** (mapeamento entre DTOs e entidades)
- **Swagger**
- **REST** Padrão arquitetural para comunicação entre cliente e servidor.

---

## 📂 Estrutura do Projeto DDD (Domain-Driven Design)
Estrutura de projeto segue uma arquitetura limpa e modular, bastante comum em aplicações .NET modernas. Com base nos arquivos abertos e nos namespaces, a organização está dividida em camadas principais.

<div>
<img width="400" height="950" alt="image" src="https://github.com/user-attachments/assets/6883075e-21b7-41b7-9f6a-8863b31fadad" />
</div>
