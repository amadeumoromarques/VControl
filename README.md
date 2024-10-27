# Volvo - Truck Control System

O **Volvo - Truck Control System** é um sistema desenvolvido para gerenciar o cadastro de caminhões e suas propriedades, proporcionando uma interface intuitiva para que o usuário possa organizar e controlar diferentes aspectos dos caminhões no sistema.

## Funcionalidades Principais

Este sistema permite ao usuário cadastrar e gerenciar:

### 1. Caminhões
  - **Código do Chassi**: Identificação única de cada caminhão.
  - **Ano de Fabricação**: Ano em que o caminhão foi produzido.
  - **Cor**: Cor do caminhão (ligada à tabela de cores).
  - **Modelo**: Modelo do caminhão (ligado à tabela de modelos).
  - **Planta**: Local de fabricação (ligado à tabela de plantas).

### 2. Modelos de Caminhões
  - **Tipo**: Categoria ou tipo específico de caminhão disponível no sistema.

### 3. Plantas de Produção
  - **Código da Planta**: Identificador único para cada planta.
  - **Nome da Planta**: Nome da planta de produção.

### 4. Cores Disponíveis
  - **Nome da Cor**: Nome da cor disponível para o cadastro dos caminhões.
  - **Código no Sistema**: Código de referência da cor no sistema.
  - **Cor em Hexadecimal**: Código hexadecimal da cor, facilitando a padronização visual.

## Video de funcionamento do sistema
Clique na imagem abaixo para visualizar as funções basicas do software em funcionamento: 

[Clique aqui para assitir o video de introdução ao sistema](https://youtu.be/B5Gc1P2i3Ug)

[![Assista ao vídeo no YouTube](https://github.com/amadeumoromarques/VControl/blob/master/System%20Pictures/0_HomePage.png)](https://youtu.be/B5Gc1P2i3Ug)

## Download do executável para abrir a aplicação
Se você deseja executar a aplicação antes de efetuar o build no seu computador, é possível efetuar o download do executável para iniciar a API e o FRONT:

- Download do executável - Arquivo do Git: WebApi_All_PublishedVersion.zip - [Clique aqui para fazer download](https://github.com/amadeumoromarques/VControl/blob/master/WebApi_All_PublishedVersion.zip)
- Video de como baixar e abrir o executável do Windows - [Clique aqui para assitir o video](https://youtu.be/CXUrOcfxP0A)

**OBS: É necessário extrair todos os arquivos do .zip para que funcione corretamente**

## Tecnologias Utilizadas

Este sistema foi construído utilizando tecnologias robustas para proporcionar desempenho e flexibilidade. Principais tecnologias e frameworks usados:

- **C# e .NET Core**: Backend para processamento de dados e operações CRUD.
- **Angular**: Frontend para criação de uma interface de usuário dinâmica e responsiva.
- **Entity Framework Core**: Mapeamento objeto-relacional (ORM) para facilitar a interação com o banco de dados.
- **OData API**: Permite a consulta de dados eficiente e flexível para o frontend.
- **Banco de Dados SQLite local**: Estrutura para armazenamento seguro e organizado dos dados.
- **Migrations**: Criação automática do banco de dados local, utilizando migrations.


## Pré-Requisitos para Baixar o Projeto e efetuar o Build da aplicação

Para configurar e rodar o projeto no seu computador, verifique se você possui as seguintes ferramentas instaladas:

### Backend (.NET)

1. **.NET SDK 6.0 ou superior**  
   - Baixe e instale o SDK do .NET 6.0 ou superior em [dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).
   - Verifique a instalação com o comando:
     ```bash
     dotnet --version
     ```

2. **Visual Studio 2022 ou Visual Studio Code**  
   - Para um desenvolvimento mais prático e suporte completo a .NET, instale o [Visual Studio 2022](https://visualstudio.microsoft.com/) (versão Community gratuita) ou [Visual Studio Code](https://code.visualstudio.com/).

### Frontend (Angular)

1. **Node.js 9.8.1 ou superior**
   - Angular requer Node.js para ser executado. Baixe e instale o Node.js em [nodejs.org](https://nodejs.org/).
   - Verifique a instalação com o comando:
     ```bash
     node --version
     ```

2. **Angular CLI**
   - Instale a CLI do Angular para criar e gerenciar o projeto Angular.
     ```bash
     npm install -g @angular/cli
     ```

3. **Visual Studio Code**  
   - Editor recomendado para projetos Angular. Baixe e instale em [Visual Studio Code](https://code.visualstudio.com/).

4. **Pacotes de Dependência**
   - Após clonar o repositório, navegue até a pasta do frontend '.\Front\ e execute:
     ```bash
     npm install
     ```
     
5. **Iniciar a aplicação do Front**
   - Após instalar os arquivos do node_module necessários para o front, continue na pasta '.\Front\ e execute:
     ```bash
     npm start
     ```

## Como efetuar o Build da aplicação?

Se deseja verificar como deve ser instalado corretamente o sistema e efetuar o build da aplicação, pode seguir os passos abaixo:
- Video mostrando como baixar o projeto e compilar ele na máquina - [Video explicando como efetuar o debug da aplicação](https://youtu.be/HryHCL3DK7s).
- Documento técnico com instruções detalhadas do passo a passo de como efetuar o build da aplicação - [Link do Download do arquivo](https://github.com/amadeumoromarques/VolvoCrud/blob/master/Documents/Efetuar%20o%20Build%20da%20aplica%C3%A7%C3%A3o%20local.pdf)

## Como executar os testes unitários?

- Vídeo explicando como rodar os testes unitários dentro do Visual Studio 2022 - [Vídeo tutorial de execução dos testes na API](https://youtu.be/82Wcdxyc0us)
- Documento técnico instruindo como realizar os testes unitários - [Link do Download do arquivo](https://github.com/amadeumoromarques/VolvoCrud/blob/master/Documents/Efetuar%20o%20Build%20da%20aplica%C3%A7%C3%A3o%20local.pdf)
