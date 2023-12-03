// TodoItem.test.js
import { fireEvent, render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import React from 'react';
import TodoItem from '../Components/TodoItem';

describe('TodoItem', () =>
{
  const mockTodo = { id: 1, name: '', isComplete: false };
  let mockSetTodos;

  beforeEach(() =>
  {
    mockSetTodos = jest.fn();
    global.fetch = jest.fn().mockResolvedValue({
      ok: true,
      headers: {
        get: jest.fn().mockReturnValue('some-header-value'), // Mock the get method
      },
      json: () => Promise.resolve({ id: mockTodo.id, name: 'Updated Task', isComplete: mockTodo.isComplete }),
    });
  });

  afterEach(() =>
  {
    jest.restoreAllMocks();
  });

  test('updates todo item on update', async () =>
  {
    render(<TodoItem todo={ mockTodo } setTodos={ mockSetTodos } />);

    // Click the edit button
    const editButton = screen.getByRole('button', { name: /Edit/i });
    fireEvent.click(editButton);

    // Type in the input field
    const input = screen.getByPlaceholderText('Task Name');
    await userEvent.type(input, 'Updated Task');

    // Click the update button
    const updateButton = screen.getByRole('button', { name: /Update/i }); // Adjust based on the actual text or aria-label of your update button
    fireEvent.click(updateButton);

    // Wait for fetch to be called
    await waitFor(() =>
    {
      expect(global.fetch).toHaveBeenCalledWith(
        `http://localhost:5288/api/TodoItems/${ mockTodo.id }`,
        {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ id: mockTodo.id, name: 'Updated Task', isComplete: mockTodo.isComplete }),
        }
      )


    });
  });
});
