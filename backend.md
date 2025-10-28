# Documentação da API - WTC

## Índice
- [Autenticação](#autenticação)
- [Usuários](#usuários)
- [Segmentos](#segmentos)
- [Campanhas](#campanhas)
- [Notas](#notas)
- [Grupos](#grupos)
- [Chat](#chat)

---

## Autenticação

### Login
Realiza a autenticação do usuário no sistema.

**Endpoint:** `POST /login`

**Request:**
```json
{
    "email": "string",
    "senha": "string",
    "profile": ["client", "operator"]
}
```

**Response:** `200 OK`
```json
{
    "token": "string",
    "user": {
        "id": "string",
        "email": "string",
        "name": "string",
        "profile": "string",
        "tags": ["string"],
        "score": "number",
        "status": "string"
    }
}
```

---

## Usuários

### Listar Usuários
Retorna uma lista de usuários com base em filtros opcionais.

**Endpoint:** `GET /users`

**Query Parameters:**
- `tags` (opcional): Array de strings para filtrar por tags
- `score` (opcional): Número para filtrar por pontuação
- `status` (opcional): String para filtrar por status

**Response:** `200 OK`
```json
{
    "users": []
}
```

---

## Segmentos

### Listar Segmentos
Retorna todos os segmentos disponíveis.

**Endpoint:** `GET /segments`

**Response:** `200 OK`
```json
{
    "id": "string",
    "title": ["string"]
}
```

---

## Campanhas

### Enviar Campanha
Envia uma campanha de marketing para um segmento específico de usuários.

**Endpoint:** `POST /send-campaigns`

**Request:**
```json
{
    "segmentId": "string",
    "message": {
        "title": "Campanha Especial",
        "body": "Participe do nosso evento exclusivo!",
        "url": "https://wtc.com/evento",
        "actions": [
            {
                "action": "btn1",
                "title": "Inscrever-se"
            },
            {
                "action": "btn2",
                "title": "Saiba Mais"
            }
        ],
        "actionUrls": {
            "btn1": "https://wtc.com/evento/inscricao",
            "btn2": "https://wtc.com/evento/detalhes",
            "abrir": "https://wtc.com/evento"
        }
    }
}
```

**Estrutura da Mensagem:**
- `title`: Título da campanha
- `body`: Corpo da mensagem
- `url`: URL principal da campanha
- `actions`: Array de ações disponíveis (botões)
- `actionUrls`: URLs associadas a cada ação

**Response:** `200 OK`

---

## Notas

### Buscar Notas do Usuário
Retorna todas as notas de um usuário específico.

**Endpoint:** `GET /notes/:userId`

**Path Parameters:**
- `userId`: ID do usuário

**Response:** `200 OK`
```json
{
    "notes": [
        {
            "id": "string",
            "userId": "string",
            "content": "string",
            "createdAt": "string"
        }
    ]
}
```

---

## Grupos

### Listar Grupos
Retorna todos os grupos disponíveis.

**Endpoint:** `GET /groups`

**Response:** `200 OK`
```json
{
    "groups": []
}
```

---

## Chat

### Enviar Mensagem
Envia uma mensagem de um usuário para outro.

**Endpoint:** `POST /send-message`

**Request:**
```json
{
    "senderId": "string",
    "receiverId": "string",
    "message": "string"
}
```

**Response:** `200 OK`

---

### Buscar Mensagens
Retorna o histórico de mensagens entre dois usuários.

**Endpoint:** `GET /messages`

**Request:**
```json
{
    "userId": "string",
    "receiverId": "string"
}
```

**Response:** `200 OK`
```json
{
    "messages": [
        {
            "senderId": "string",
            "receiverId": "string",
            "message": "string",
            "timestamp": "string"
        }
    ]
}
```

---

## Códigos de Status HTTP

| Código | Descrição |
|--------|-----------|
| 200 | OK - Requisição bem-sucedida |
| 400 | Bad Request - Requisição inválida |
| 401 | Unauthorized - Não autenticado |
| 403 | Forbidden - Sem permissão |
| 404 | Not Found - Recurso não encontrado |
| 500 | Internal Server Error - Erro no servidor |

---

**Versão:** 1.0.0  
**Data:** Outubro 2025  
**Projeto:** FIAP Challenge - WTC
```