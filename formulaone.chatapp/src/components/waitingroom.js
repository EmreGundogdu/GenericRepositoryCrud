import { useState } from "react";
import { Button, Col, Form, Row } from "react-bootstrap";

const WaitingRoom = ({ joinChatRoom }) => {
    const [username, setUsername] = useState("");
    const [chatRoom, setChatRoom] = useState("");

    return <Form onSubmit={(e) => {
        e.preventDefault();
        joinChatRoom(username, chatRoom);
    }
    }>

        <Row className="px-5 my-5">
            <Col sm='12'>
                <Form.Group placeholder='Username' onChange={e => setUsername(e.target.value)}> </Form.Group>
                <Form.Group placeholder='Chatroom' onChange={e => setChatRoom(e.target.value)}> </Form.Group>
            </Col>
            <Col sm='12'>
            <hr />
            <Button variant="success" type="submit">Join</Button>
            </Col>
        </Row>
    </Form>
}

export default WaitingRoom;