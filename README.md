# 🧠 BrainZap - Proyecto estilo Kahoot en C#

> Un juego de preguntas y respuestas multijugador, inspirado en Kahoot, desarrollado en C#.

---

## ⚙️ Requisitos

Para ejecutar correctamente el proyecto, debes colocar el archivo del certificado:

```
elMeuCertificat.pfx
```

En la carpeta:

```
./debug
```

---

## 🔌 Comunicación Cliente-Servidor

A continuación se describe el protocolo de comunicación entre el cliente y el servidor:

### 🧍 Cliente ➝ Servidor

- Registro del usuario:
  ```
  NICK|<nick>|<ip>|<puerto>
  ```

### 🖥️ Servidor ➝ Cliente

- Si el nick está **ocupado**:
  ```
  NICK|<nick>|ERROR
  ```

- Si el nick está **disponible**:
  ```
  NICK|<nick>|OK
  ```

---

## ❓ Preguntas y Respuestas

### 📤 Servidor ➝ Cliente

- Envío de pregunta:
  ```
  PREGUNTA|<pregunta>|<opción1>|<opción2>|<opción3>|<opción4>
  ```

### 📥 Cliente ➝ Servidor

- Envío de respuesta:
  ```
  RESPUESTA|<nick>|<pregunta>|<respuesta>
  ```

### ✅ Servidor ➝ Cliente

- Resultado de la respuesta:
  ```
  RESULTADO|<nick>|<respuesta>|<puntos>|<nick1>:<puntos1>,<nick2>:<puntos2>,<nick3>:<puntos3>
  ```

---

## 🏁 Fin de la Partida

Al finalizar la partida, el servidor envía la puntuación final:

```
FINPARTIDA|<nick1>:<puntos1>,<nick2>:<puntos2>,<nick3>:<puntos3>
```

---

## 👨‍💻 Autores

- [@mbaeta8](https://github.com/mbaeta8)  
- [@QueroXD](https://github.com/QueroXD)
