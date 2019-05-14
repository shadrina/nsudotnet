import React, { Component } from 'react';
import { InputFormRow } from '../InputFormRow.js'
import { Button, Modal, ModalHeader, ModalBody, Container } from 'reactstrap';
import axios from 'axios';

export class SignUpModal extends Component {
    constructor(props) {
        super(props);

        this.toggle = this.toggle.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
        this.loginOnChange = this.loginOnChange.bind(this);
        this.passwordOnChange = this.passwordOnChange.bind(this);
        this.state = {
            isOpen: false,
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
        const url = '/users';
        axios({
            method: 'post',
            url: url,
            data: {
                login: this.state.login,
                password: this.state.password
            }
        })
            .then(response => {
                console.log("Регистрация прошла успешно");
            })
            .catch(error => {
                if (error.response) {
                    console.log(error.response.status);
                }
                else if (error.request) {
                    console.log(error.request);
                }
                else {
                    console.log(error.message);
                }

            })
    }

    renderRegistrationForm() {
        return (
            <form id="registration-form" method="post" onSubmit={this.onRegistrationSubmit}>
                <div id="registration-form" className="input-form">
                    <InputFormRow id="reg-login" onChange={this.loginOnChange} label="Логин" type="text"/>
                    <InputFormRow id="reg-password" onChange={this.passwordOnChange} label="Пароль" type="password"/>
                </div>
                <Container className="button-box mt-2">
                    <Button className="mr-1" color="primary" type="submit">Зарегистрироваться</Button>
                    <Button className="mr-1" type="button" onClick={this.props.toggle}>Отмена</Button>
                </Container>
            </form>
        )
    };

    render() {
        if (this.props.isOpen) {
            return (
                <Modal isOpen={this.props.isOpen} toggle={this.props.toggle}>
                    <ModalHeader toggle={this.props.toggle}>Регистрация в системе</ModalHeader>
                    <ModalBody>
                        { this.renderRegistrationForm() }
                    </ModalBody>
                </Modal>
            )
        }
        else {
            return null;
        }
    }

}