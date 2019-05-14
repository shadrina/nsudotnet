import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { SignUpModal } from "../modals/SignUpModal";
import {SignInModal} from "../modals/SignInModal";
import { Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.toggleSignIn = this.toggleSignIn.bind(this);
    this.toggleSignUp = this.toggleSignUp.bind(this);
    this.toggleDropDown = this.toggleDropDown.bind(this);
    this.state = {
      collapsed: true,
      siModal: false,
      suModal: false,
      dropdownOpen: false,
    };
  }

  toggleDropDown() {
      this.setState(prevState => ({
          dropdownOpen: !prevState.dropdownOpen
      }));
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
            <SignInModal isOpen={this.state.siModal} toggle={this.toggleSignIn} />
        )
    }

    renderSignUpModal() {
        return (
            <SignUpModal isOpen={this.state.suModal} toggle={this.toggleSignUp} />
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
                  <NavLink tag={Link} className="text-dark" to="/new-request">Новая задача</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/request-list">Список выполняющихся задач</NavLink>
                </NavItem>
                <NavItem>
                    <Dropdown isOpen={this.state.dropdownOpen} toggle={this.toggleDropDown}>
                        <DropdownToggle color="light" caret>
                            Авторизация
                        </DropdownToggle>
                        <DropdownMenu>
                            <DropdownItem>
                                {siModal && this.renderSignInModal()}
                                <NavLink href="#" tag={Link} className="text-dark" to="/login" onClick={this.toggleSignIn}>Войти</NavLink>
                            </DropdownItem>
                            <DropdownItem>
                                {suModal && this.renderSignUpModal()}
                                <NavLink href="#" tag={Link} className="text-dark" to="/registration" onClick={this.toggleSignUp}>Зарегистрироваться</NavLink>
                            </DropdownItem>
                        </DropdownMenu>
                    </Dropdown>
                </NavItem>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
