import React, { Component } from 'react';
import { InputFormRow } from '../InputFormRow.js'
import { Button, Modal, ModalHeader, ModalBody, Container } from 'reactstrap';
import axios from 'axios';

export class SignInModal extends Component {
    constructor(props) {
        super(props);

        this.toggle = this.toggle.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
        this.loginOnChange = this.loginOnChange.bind(this);
        this.passwordOnChange = this.passwordOnChange.bind(this);
        this.state = {
            isOpen: props.isOpen,
            login: '',
            password: '',
        };
    }

    toggle() {
        this.setState(prevState => ({
            isOpen: !prevState.isOpen
        }))
    };

    loginOnChange(e) {
        this.setState({ login: e.target.value });
    };
    passwordOnChange(e) {
        this.setState({ password: e.target.value });
    };

    onSubmit() {
        let url = '/api/SampleData/SignInRequest';
        axios({
            method: 'post',
            url: url,
            data: {
                login: this.state.login,
                password: this.state.password
            }
        })
            .then(response => {
                return response.data; // if return data (token) has the text/plain content-type
            })
            .then(token => {
                localStorage.setItem('token', token);
            })
            .catch(error => console.error(error))
    }

    renderLoginForm() {
        return (
            <form id="registration-form">
                <div id="registration-form" className="input-form">
                    <InputFormRow id="reg-login" onChange={this.loginOnChange} label="Логин" type="text"/>
                    <InputFormRow id="reg-password" onChange={this.passwordOnChange} label="Пароль" type="password"/>
                </div>
                <Container className="button-box mt-2">
                    <Button className="mr-2" color="primary" onClick={this.onSubmit}>Войти</Button>
                    <Button className="gapr-15" type="button" onClick={this.props.toggle}>Отмена</Button>
                </Container>
            </form>
        )
    };

    render() {
        return (
          <Modal isOpen={this.state.isOpen} toggle={this.props.toggle}>
            <ModalHeader toggle={this.props.toggle}>Вход в систему</ModalHeader>
            <ModalBody>
              { this.renderLoginForm() }
              </ModalBody>
          </Modal>
        )
    }

}