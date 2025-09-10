// ========== Import dependencies ========== //
import express from "express";
import fs from "fs/promises";
import cors from "cors";
import { randomUUID } from "crypto";

// ========== Setup Express App ========== //
const app = express();
const PORT = 3000;

// Middleware
app.use(express.json()); // Parse JSON request bodies
app.use(cors()); // Enable CORS for all routes - allow requests from any origin

// ========== File Helper Operations ========== //
// Helper: Read messages from file
async function readMessages() {
  const data = await fs.readFile("data/messages.json");
  const messages = JSON.parse(data);
  return messages;
}

// Helper: Write messages to file
function writeMessages(messages) {
  fs.writeFile("data/messages.json", JSON.stringify(messages, null, 2));
}

// ========= Define API Endpoints ========== //
app.get("/", (req, res) => {
  res.send("Node.js Messages REST API 🎉dev");
});

// GET /messages - get all messages
app.get("/messages", async (req, res) => {
  const page = parseInt(req.query.page) || 1;
  const limit = parseInt(req.query.limit) || 3;
  let messages = [];

  //Read messages with error handling
  try { 
    messages = await readMessages();
  }
  catch(error){
    return res.status(500).json({ error: "Failed to read messages" });
  }

  //Search text and sender
  if(req.query.search) messages = messages.filter(msg => msg.text.toLowerCase().includes(req.query.search));
  if(req.query.sender) messages = messages.filter(msg => msg.sender.toLowerCase().includes(req.query.sender));
  
  const messageCount = messages.length;

  //Sort date
  if(req.query.sort === "-date") messages = messages.sort((a, b) => new Date(b.date) - new Date(a.date));
  else messages = messages.sort((a, b) => new Date(a.date) - new Date(b.date));

  //Slice for pagination
  const startIndex = (page - 1) * limit;
  const endIndex = page * limit;
  messages = messages.slice(startIndex, endIndex);

  res.json({
    Messagecount: messageCount,
    PageNumber: page,
    MessagesPerPage: limit,
    Messages: messages})
});

// GET /messages/:id - get message by id
app.get("/messages/:id", async (req, res) => {
  let messages = [];

  try{
    messages = await readMessages();
  } catch(error){
    return res.status(500).json({ error: "Failed to read messages" });
  }

  const messageId = req.params.id;
  
  const message = messages.find(message => message.id === messageId);
  if (!message) {
    return res.status(404).json({ error: "Message not found" });
  }

  res.json(message);
});

// POST /messages - add new message
app.post("/messages", async (req, res) => {
  let messages = [];
  
  try{
    messages = await readMessages();
  } catch(error){
    return res.status(500).json({ error: "Failed to read messages" });
  }

  const { text, sender } = req.body;
  if (!text || !sender) {
    return res.status(400).json({ error: "Text and sender are required" });
  }

  const newMessage = {
    id: randomUUID(),
    date: new Date().toISOString(),
    text,
    sender
  };

  messages.push(newMessage);
  writeMessages(messages);

  res.status(201).json(newMessage);
});

// PUT /messages/:id - update message
app.put("/messages/:id", async (req, res) => {
  let messages = [];

  try {
    messages = await readMessages();
  } catch (error) {
    return res.status(500).json({ error: "Failed to read messages" });
  }

  const messageId = req.params.id;
  const message = messages.find(message => message.id === messageId);
  if (!message) {
    return res.status(404).json({ error: "Message not found" });
  }

  const { text, sender } = req.body;
  if (!text || !sender) {
    return res.status(400).json({ error: "Text and sender are required" });
  }

  message.text = text;
  message.sender = sender;

  writeMessages(messages);
  res.json(message);
});

// DELETE /messages/:id - delete message
app.delete("/messages/:id", async (req, res) => {
  let messages = []

  try{
    messages = await readMessages();
  } catch (error) {
    return res.status(500).json({ error: "Failed to read messages" });
  }

  const messageId = req.params.id;

  messages = messages.filter(message => message.id !== messageId);

  writeMessages(messages);
  res.json(messages);
});

// ========== Start the server ========== //
app.listen(PORT, () => {
  console.log(`App listening on port ${PORT}`);
  console.log(`App is running on http://localhost:${PORT}`);
  console.log(`Messages Endpoint http://localhost:${PORT}/messages`);
});
