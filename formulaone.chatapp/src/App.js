import { Col, Container, Row } from 'react-bootstrap';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useState } from 'react';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import WaitingRoom from './components/Waitingroom';
import ChatRoom from './components/ChatRoom';

function App() {

  const [conn, setConnection] = useState();
  const [messages, setMessages] = useState([]);

  const joinChatRoom = async (username, chatRoom) => {
    try {
      const conn = new HubConnectionBuilder().withUrl("http://localhost:5236/chat").configureLogging(LogLevel.Information).build();
      await conn.start();
      await conn.invoke("JoinSpecificChatRoom", {
        userName: username,
        chatRoom: chatRoom
      });
      setConnection(conn);

      conn.on("JoinSpecificChatRoom", (user, message) => {
        console.log(`${user}: ${message}`);
      });

      conn.on("ReceiveSpecificMesssage", (username, message) => {
        setMessages(messages => [...messages, { username, message }]);
      });
    } catch (error) {
      console.error("Error joining chat room:", error);
      if (conn) {
        await conn.stop();
      }
    }
  }

  const sendMessage = async (message) => {
    if (conn) {
      try {
        await conn.invoke("SendMessage", message);
      } catch (error) {
        console.error("Error sending message:", error);
      }
    }
  }


  return (
    <div>
      <main>
        <Container>
          <Row class='px-5 my-5'>
            <Col sm='12'>
              <h1 className='font-weight-light'>Wellcome to the f1 chat app</h1>
            </Col>
          </Row>
          {!conn
            ? <WaitingRoom joinChatRoom={joinChatRoom}></WaitingRoom>
            : <ChatRoom messages={messages} sendMessage={sendMessage}></ChatRoom>
          }
        </Container>
      </main>
    </div>
  );
}

export default App;
