import { library } from '@fortawesome/fontawesome-svg-core';
import { faFontAwesome, faTwitter } from '@fortawesome/free-brands-svg-icons';
import { faFloppyDisk, faPenToSquare, faPlus, faRectangleXmark, faTrash, fas } from '@fortawesome/free-solid-svg-icons';
import 'bootstrap/dist/css/bootstrap.min.css';
import React from 'react';
import './App.css';
import TodoList from './Components/TodoList';

library.add(fas, faTrash, faPlus, faFloppyDisk, faRectangleXmark, faPenToSquare, faTwitter, faFontAwesome);

const App = () => <TodoList />

export default App;
