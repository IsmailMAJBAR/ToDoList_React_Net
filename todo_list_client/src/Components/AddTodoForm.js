import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useState } from 'react';

const AddTodoForm = ({ setTodos }) =>
{
  const [ name, setName ] = useState('');

  const handleSubmit = (event) =>
  {
    event.preventDefault();
    fetch('http://localhost:5288/api/TodoItems', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ name, isComplete: false })
    })
      .then(response => response.json())
      .then(newTodo =>
      {
        // Update the UI list after insert to database
        setTodos(todos => [ ...todos, newTodo ]);
        setName("");
      });
  };

  return (
    <form role="form" onSubmit={ handleSubmit } className="mb-3">
      <div className="input-group">
        <input
          type="text"
          value={ name }
          onChange={ (e) => setName(e.target.value) }
          className="form-control"
          placeholder="Add a new task"
        />
        <button className="btn btn-primary" type="submit">
          <FontAwesomeIcon icon={ faPlus } />
        </button>
      </div>
    </form>
  );
};

export default AddTodoForm;
