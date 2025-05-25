const MessageContainer = ({messages})=>{
    return <div>
        {
            messages.map((msg, index) => (
               <table striped bordered>
                <tr key={index}>
                    <td>{msg.username} - {msg.message }</td>
                </tr>
               </table>
            ))  
        }
    </div>
}

export default MessageContainer;