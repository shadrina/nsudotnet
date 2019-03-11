import React, { Component } from 'react';

export class RequestList extends Component {
  static displayName = RequestList.name;

  constructor (props) {
    super(props);
    this.state = {
      requests: [],
      loading: true
    };

    this.updateTable = this.updateTable.bind(this);
    this.renderRequestsTable = this.renderRequestsTable.bind(this);
    this.onDeleteButtonClicked = this.onDeleteButtonClicked.bind(this);

    this.updateTable();
  }

  updateTable() {
    fetch('api/SampleData/Requests')
      .then(response => response.json())
      .then(data => {
        this.setState({ requests: data, loading: false });
      })
      .catch(error => console.error(error));
  }

  onDeleteButtonClicked(id) {
    fetch('api/SampleData/DeleteRequest', {
      method: 'DELETE',
      body: id,
      headers: {
        "Content-Type": "application/json"
      }
    })
      .then(this.updateTable)
      .catch(error => console.error(error))
  }

  renderRequestsTable() {
    return (
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>Название</th>
            <th>Вместимость</th>
            <th>Максимальная ценность</th>
            <th>Процент выполнения</th>
            <th>Статус</th>
            <th />
            <th />
          </tr>
        </thead>
        <tbody>
          {this.state.requests.map(request =>
            <tr key={request.requestId}>
              <td>{request.name}</td>
              <td>{request.capacity}</td>
              <td>{request.answer}</td>
              <td>{request.percentage}</td>
              <td>{request.status ? "Выполнено" : "В процессе"}</td>
              <td>
                <button type="button" className="btn btn-danger" onClick={_ => this.onDeleteButtonClicked(request.requestId)}>
                  Удалить
                </button>
              </td>
              <td><button type="button" className="btn btn-primary">Новая задача</button>
              </td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render () {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderRequestsTable();

    return (
      <div>
        <h1>Список выполняющихся задач</h1>
        <p>Здесь ты увидишь, какие запросы пользователи отправляли в систему.</p>
        {contents}
      </div>
    );
  }
}
