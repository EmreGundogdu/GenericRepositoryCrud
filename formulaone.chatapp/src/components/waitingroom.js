import { useState } from "react";
import { Button, Col, Form, Row } from "react-bootstrap";

const WaitingRoom = ({ joinChatRoom }) => {
    const [UserName, setUsername] = useState("");
    const [ChatRoom, setChatRoom] = useState("");

    return <Form onSubmit={(e) => {
        e.preventDefault();
        joinChatRoom(UserName, ChatRoom);
    }}>
         <Row className="px-5 my-5">
            <Col sm={12}>
               <Form.Group>
                 <Form.Control placeholder='UserName' onChange={e => setUsername(e.target.value)}/>
                <Form.Control placeholder='ChatRoom' onChange={e => setChatRoom(e.target.value)}/>
                </Form.Group>
            </Col>
            <Col sm='12'>
            <hr />
            <Button variant="success" type="submit">Join</Button>
            </Col>
        </Row>
    </Form>
}

export default WaitingRoom;