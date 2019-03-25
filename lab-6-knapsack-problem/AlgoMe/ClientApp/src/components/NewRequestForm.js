import React, { Component } from 'react';
import axios from 'axios';

export class NewRequestForm extends Component {
  static displayName = NewRequestForm.name;

  constructor (props) {
    super(props);
    this.state = {
      name: "",
      capacity: 0,
      parameters: [],
    };

    this.onRequestNameChange = this.onRequestNameChange.bind(this);
    this.onCapacityChange = this.onCapacityChange.bind(this);
    this.addParameter = this.addParameter.bind(this);
    this.setName = this.setName.bind(this);
    this.setPrice = this.setPrice.bind(this);
    this.setWeight = this.setWeight.bind(this);
    this.onSubmit = this.onSubmit.bind(this);
  }

  addParameter(name, price, weight) {
    let newParams = this.state.parameters;
    newParams.push({name: name, price: price, weight:weight});
    this.setState({
      parameters: newParams
    });
  }

  onRequestNameChange(newName) {
    this.setState({
      name: newName
    });
  }

  onCapacityChange(newCapacity) {
    this.setState({
      capacity: newCapacity
    });
  }

  setName(idx, name) {
    let newParams = this.state.parameters;
    newParams[idx].name = name;
    this.setState({
      parameters: newParams
    });
  }

  setPrice(idx, price) {
    let newParams = this.state.parameters;
    newParams[idx].price = price;
    this.setState({
      parameters: newParams
    });
  }

  setWeight(idx, weight) {
    let newParams = this.state.parameters;
    newParams[idx].weight = weight;
    this.setState({
      parameters: newParams
    });
  }

  onSubmit() {
    console.log(this.state);
    axios.post('/api/SampleData/PostRequest', this.state)
      .then(function (response) {
        console.log(response);
      })
      .catch(function (error) {
        console.log(error);
      });
  }

  render () {
    return (
      <div>
        <h1>Новый запрос</h1>

        <form>
          <div className="form-group">
            <label htmlFor="formGroupExampleInput">Название</label>
            <input type="text"
                   className="form-control"
                   id="formGroupExampleInput"
                   placeholder="Запрос такой-то..."
                   onChange={e => this.onRequestNameChange(e.target.value)} />
          </div>
          <div className="form-group">
            <label htmlFor="formGroupExampleInput2">Вместимость рюкзака</label>
            <input type="text"
                   className="form-control"
                   id="formGroupExampleInput2"
                   placeholder="100"
                   onChange={e => this.onCapacityChange(parseInt(e.target.value))} />
          </div>
          <button type="button" className="btn btn-primary" onClick={_ => this.addParameter("", "", "")}>Добавить предмет</button>
          {
            this.state.parameters.map((val, idx)=> {
              return (
                <div key={idx}>
                  <br />
                  <div className="row">
                    <div className="col">
                      <input type="text"
                             className="form-control"
                             placeholder="Назввание предмета"
                             onChange={e => this.setName(idx, e.target.value)} />
                    </div>
                    <div className="col">
                      <input type="text"
                             className="form-control"
                             placeholder="Ценность"
                             onChange={e => this.setPrice(idx, parseInt(e.target.value))} />
                    </div>
                    <div className="col">
                      <input type="text"
                             className="form-control"
                             placeholder="Вес"
                             onChange={e => this.setWeight(idx, parseInt(e.target.value))} />
                    </div>
                  </div>
                </div>
              )
            })
          }
          <br /><br />
          <button type="button" className="btn btn-primary" onClick={this.onSubmit}>Отправить</button>
        </form>
      </div>
    );
  }
}
