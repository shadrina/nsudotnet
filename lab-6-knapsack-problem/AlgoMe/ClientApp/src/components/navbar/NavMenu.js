import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Input, Label, Form, FormGroup } from 'reactstrap';
import { InputFormRow } from '../InputFormRow.js'

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.toggleSignIn = this.toggleSignIn.bind(this);
    this.toggleSignUp = this.toggleSignUp.bind(this);
    this.state = {
      collapsed: true,
      siModal: false,
      suModal: false
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

    toggleSignIn() {
        this.setState(prevState => ({
            siModal: !prevState.siModal
        }));
    };
    toggleSignUp() {
        this.setState(prevState => ({
            suModal: !prevState.suModal
        }));
    };

    renderSignInModal() {
        return (
            <Modal isOpen={this.state.siModal} toggle={this.toggleSignIn} className={this.props.className}>
                <ModalHeader toggle={this.toggleSignIn}>Sign In</ModalHeader>
                <ModalBody>
                    <InputFormRow label="Login" type="text"/>
                    <InputFormRow label="Password" type="password"/>
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={this.toggleSignIn}>Sign In</Button>{' '}
                    <Button color="secondary" onClick={this.toggleSignIn}>Cancel</Button>
                </ModalFooter>
            </Modal>
        )
    }

    renderSignUpModal() {
        return (
            <Modal isOpen={this.state.suModal} toggle={this.toggleSignUp} className={this.props.className}>
                <ModalHeader toggle={this.toggleSignUp}>Sign Up</ModalHeader>
                <ModalBody>
                    <InputFormRow label="Login" type="text"/>
                    <InputFormRow label="Password" type="password"/>
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={this.toggleSignUp}>Sign Up</Button>{' '}
                    <Button color="secondary" onClick={this.toggleSignUp}>Cancel</Button>
                </ModalFooter>
            </Modal>
        )
    }

  render () {
    const { suModal, siModal } = this.state;
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">AlgoMe</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Привет!</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/counter">Новая задача</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/fetch-data">Список выполняющихся задач</NavLink>
                </NavItem>
                  <NavItem>
                      {siModal && this.renderSignInModal()}
                      <NavLink href="#" tag={Link} className="text-dark" to="/login" onClick={this.toggleSignIn}>Sign in</NavLink>
                  </NavItem>
                  <NavItem>
                      {suModal && this.renderSignUpModal()}
                      <NavLink href="#" tag={Link} className="text-dark" to="/registration" onClick={this.toggleSignUp}>Sign up</NavLink>
                  </NavItem>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
