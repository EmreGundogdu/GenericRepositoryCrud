import { Col, Container, Row } from 'react-bootstrap';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import WaitingRoom from './components/waitingroom';
import { useState } from 'react';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

function App() {

  const[conn,setConnection] = useState();

  const joinChatRoom =async  (username, chatRoom) => {
    try {
      const conn = new HubConnectionBuilder().withUrl("http://localhost:5236/chat").configureLogging(LogLevel.Information ).build();

      conn.on("JoinSpecificChatRoom", (user, message) => {
        console.log(`${user}: ${message}`);
      }
      );  
      await conn.start();
      await conn.invoke("JoinSpecificChatRoom", {
  userName: username,
  chatRoom: chatRoom
});
      setConnection(conn);
    } catch (error) {
      console.error("Error joining chat room:", error);
      if (conn) {
        await conn.stop();
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
          <WaitingRoom joinChatRoom={joinChatRoom}></WaitingRoom>
        </Container>
      </main>
    </div>
  );
}

export default App;
