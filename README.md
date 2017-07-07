# Basic Authentication Web API 2

### Requisitos necessários:

* IIS
* .NET 4.5
* Visual Studio 2015

Deve publicar via iis  o conteudo do arquivo [v1.rar](./Publicacao/).
## Como Rodar 

### Autenticacao Anônima
GET http://localhost:59854/api/teste/anonimo

200 OK
```
{"Hello World"}
```

### Autenticação Basica
"Authorization" : "Basic dGVzdGU6dGVzdGU="

GET http://localhost:59854/api/teste/autenticado

200 OK
```
{"Hello World autenticado"}
```
