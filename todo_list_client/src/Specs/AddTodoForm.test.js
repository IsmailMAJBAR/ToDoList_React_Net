// AddTodoForm.test.js
import { fireEvent, render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import React from 'react';
import AddTodoForm from '../Components/AddTodoForm';

describe('AddTodoForm', () =>
{
  beforeEach(() =>
  {
    global.fetch = jest.fn(() =>
      Promise.resolve({
        json: () => Promise.resolve({ name: 'New Task', isComplete: false })
      })
    );
  });

  afterEach(() =>
  {
    jest.resetAllMocks();
  });

  test('check the fetch been called', async () =>
  {
    const setTodos = jest.fn();

    const { container } = render(<AddTodoForm setTodos={ setTodos } />);

    const inputElement = screen.getByPlaceholderText('Add a new task');
    await userEvent.type(inputElement, 'New Task');

    const form = container.querySelector('form');
    fireEvent.submit(form);

    await waitFor(() =>
    {
      expect(fetch).toHaveBeenCalledTimes(1);
    });
  });
});
