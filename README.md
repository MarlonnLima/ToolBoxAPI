# ToolBoxAPI

ToolBox is a unified service under development that helps the user with tooling to development, design and grammar

## Routes

### **[GET] /{hash}**

* **Description:** Redirect the caller to not shortened link
* **Parâmetros:**
  * **Query:**
  * **Path:**
    * hash: string - the given code of the destination
  * **Body:**
* **Headers:**
* **Example:**
  * **Requisition:**
    ```
    GET /5BDB536D
    ```
  * **Resposta:**
    ```json
    Status code 304 that redirects you to the destination.
    ```

### **[POST] /short**

* **Description:** short the link
* **Parâmetros:**
  * **Query:**
  * **Path:**
  * **Body:**
  * id: int - the id of register (can be ignored because it will not used)
  * hash: string - the given code of the destination
  * destination: string - the destination link
* **Headers:**
* **Example:**
  * **Requisition:**
    ```
    POST /short

    {
      "id": 0,
      "hash": "",
      "destination": "https://yourlink.com/qwopdiwqwqepowupoawudwa?page=2"
    }
    ```
  * **Resposta:**
    ```json
      {
        "data": {
          "id": 1,
          "hash": "5BDB536D",
          "destination": "https://yourlink.com/qwopdiwqwqepowupoawudwa?page=2"
        },
        "error": {
          "hasOcurred": false,
          "message": ""
        }
      }    
    ```
