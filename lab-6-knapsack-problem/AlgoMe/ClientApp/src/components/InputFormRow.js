import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Input, Label, Form, FormGroup } from 'reactstrap';
import { Link } from 'react-router-dom';

export class InputFormRow extends React.Component {
    constructor(props) {
        super(props);
        this.myRef = null;
    }

    render() {
        const { label, ...rest } = this.props;
        return (
            <Container onClick={this.handleClick}>
                <Label className="label">{label}</Label>
                <Input {...rest} ref={r => this.myRef = r} />
            </Container>
        );
    }

    handleClick = () => {
        this.myRef.focus();
    };
}