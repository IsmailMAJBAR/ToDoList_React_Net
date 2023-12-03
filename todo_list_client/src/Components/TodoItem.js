import { faRectangleXmark, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useState } from 'react';


const TodoItem = ({ todo, setTodos }) =>
{
  const [ isEditing, setIsEditing ] = useState(false);
  const [ updatedName, setUpdatedName ] = useState(todo.name);
  const [ isComplete, setIsComplete ] = useState(todo.isComplete);

  const handleEdit = () =>
  {
    setIsEditing(true);
  };

  const handleUpdate = () =>
  {
    const updatedTodo = { ...todo, name: updatedName, isComplete };

    fetch(`http://localhost:5288/api/TodoItems/${ todo.id }`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(updatedTodo),
    })
      .then(response =>
      {
        return response.headers.get('Content-Length') > 0
          ? response.json()
          : null;
      })
      .then(() =>
      {
        setTodos(todos => todos.map(t => (t.id === todo.id ? updatedTodo : t)));
        setIsEditing(false);
      })
    //.catch(error => console.error('Error:', error));
  };

  const handleDelete = () =>
  {
    fetch(`http://localhost:5288/api/TodoItems/${ todo.id }`, {
      method: 'DELETE',
    }).then(() =>
    {
      setTodos(todos => todos.filter(t => t.id !== todo.id));
    });
  };

  return (
    <div className={ `list-group-item ${ isComplete ? 'list-group-item-success' : '' }` }>
      { isEditing ? (
        <div className="d-flex justify-content-between align-items-center">
          <div className="input-group">
            <div className="input-group-text">
              <input
                className="form-check-input mt-0"
                type="checkbox"
                checked={ isComplete }
                onChange={ (e) => setIsComplete(e.target.checked) } // Updated line
              />
            </div>
            <input
              type="text"
              name="inputUpdate"
              placeholder="Task Name"
              className="form-control"
              value={ updatedName }
              onChange={ (e) => setUpdatedName(e.target.value) }
            />
            <div className=" input-group-append">
              <button className="btn btn-success"
                name="btnUpdate" onClick={ handleUpdate }>Update
              </button>&nbsp;
              <button className="btn btn-danger" onClick={ () => setIsEditing(false) }><FontAwesomeIcon icon={ faRectangleXmark } /></button>
            </div>
          </div>
        </div>
      ) : (
        <div className="d-flex justify-content-between align-items-center">
          <span className={ `me-auto todo-name ${ isComplete ? 'text-decoration-line-through' : '' }` } onClick={ handleEdit }
            data-toggle="tooltip" data-placement="bottom" title="Edit">
            { todo.name }
          </span>
          <button className="btn btn-primary btn-sm"
            name="btnEdit"
            onClick={ handleEdit }>
            Edit
          </button>&nbsp;
          <button
            className="btn btn-danger btn-sm"
            onClick={ () =>
            {
              if (window.confirm("Are you sure you want to delete?"))
              {
                handleDelete();
              }
            }
            }
          >
            <FontAwesomeIcon icon={ faTrash } />
          </button>
        </div>
      ) }
    </div>
  );
};

export default TodoItem;
