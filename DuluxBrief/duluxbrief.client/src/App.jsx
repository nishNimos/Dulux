import { useEffect, useState } from 'react';
import './App.css';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
    const [validated, setValidated] = useState(false);
    const [selectedFile, setSelectedFile] = useState(null);

    const handleFileChange = (event) => {
        const file = event.target.files[0];
        if (file && file.type === 'application/pdf') {
            setSelectedFile(file);
        } else {
            alert('Please select a valid PDF file.');
            setSelectedFile(null);
        }
    }
    const handleSubmit = async (event) => {
        const form = event.currentTarget;
        event.preventDefault();

        if (form.checkValidity() === false) {
            event.stopPropagation();
        }
        else {
            const formData = new FormData();
            formData.append('file', selectedFile);
            const queryString = "?student.name=" + encodeURIComponent(form.elements.formName.value) +
                "&student.email=" + encodeURIComponent(form.elements.formEmail.value);
            const headers = new Headers();
            headers.append('Access-Control-Allow-Origin', 'http://localhost:5176');
            const response = await fetch('http://localhost:5176/Student/AddStudent' + queryString, {
                method: 'POST',
                body: formData,
                headers: headers
            }).then(response => {
                if (response.ok) {
                    console.log('Student added successfully');
                    alert('Student added successfully');
                    form.reset();
                } else {
                    console.error('Error adding student');
                }
            })
                .catch(error => {
                    console.error('Error:', error);
                });
        }
        setValidated(true);

    }

    return (
        <Container>
            <h1>Dulux Brief Student Data</h1>
            <Form noValidate validated={validated} onSubmit={handleSubmit}>
                <Row className="mb-3">
                    <Form.Group as={Col} controlId="formName">
                        <Form.Label>Name</Form.Label>
                        <Form.Control type="text" required placeholder="Enter your name" />
                    </Form.Group>
                    <Form.Group as={Col} controlId="formEmail">
                        <Form.Label>Email</Form.Label>
                        <Form.Control type="email" required placeholder="Enter your email" />
                    </Form.Group>
                </Row>
                <Form.Group className="mb-3" controlId="formTranscript">
                    <Form.Label>Transcript</Form.Label>
                    <Form.Control required type="file" accept=".pdf" onChange={handleFileChange} />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Button type="submit">Submit</Button>
                </Form.Group>
            </Form>
        </Container>
    );
}

export default App;