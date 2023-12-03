import 'bootstrap/dist/css/bootstrap.min.css'; // Import Bootstrap CSS
import React, { useEffect, useState } from 'react';
import AddTodoForm from './AddTodoForm.js';
import TodoItem from './TodoItem';

const TodoList = () =>
{
  const [ todos, setTodos ] = useState([]);

  useEffect(() =>
  {
    fetch('http://localhost:5288/api/TodoItems')
      .then(response => response.json())
      .then(data => setTodos(data));
  }, []);

  return (
    <div className="container mt-5">
      <div className="row justify-content-center">
        <div className="col-md-6">
          <h2 className="text-center mb-4">Todo List</h2>
          <AddTodoForm setTodos={ setTodos } />
          <div className="list-group">
            { todos.map(todo => (
              <TodoItem key={ todo.id } todo={ todo } setTodos={ setTodos } />
            )) }
          </div>
        </div>
      </div>
    </div>
  );
};

export default TodoList;
