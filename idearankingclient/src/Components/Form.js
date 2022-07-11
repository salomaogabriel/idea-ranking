// import './App.css';
import './form.css';
import { useNavigate} from "react-router-dom";
import { useState, useEffect } from 'react';
import Creatable, { useCreatable } from 'react-select/creatable';
function Form() {


const options = [
  { value: 'chocolate', label: 'Chocolate' },
  { value: 'strawberry', label: 'Strawberry' },
  { value: 'vanilla', label: 'Vanilla' }
]
const customStyles = {
  option: (provided, state) => ({
    ...provided,
    BorderColor:'transparent',
    color: '#E1E1E6;',

  }),
  input: (provided, state) => ({
    ...provided,
    color: '#E1E1E6',

  }),
  control: (provided, state) => ({
    ...provided,
    width: '100%',
    borderColor:'transparent',
    color: '#E1E1E6',


  }),
  menu: (provided, state) => ({
    ...provided,
    color: '#E1E1E6;',
    padding: 10,
    


  }),
  multiValue: (provided, state) => ({
    ...provided,
    backgroundColor: '#202024',
    color: '#E1E1E6',
    fontSize: '18px'


  }),
  multiValueLabel: (provided, state) => ({
    ...provided,
    backgroundColor: '#202024',
    color: '#E1E1E6',


  })};


  const [titleError, setTitleError] = useState("");
  const [categories, setCategories] = useState([]);
  const [descriptionError, setDescriptionError] = useState("");
  const [newCategories, setNewCategories] = useState([]);
  const [existingCategories, setExistigCategories] = useState([]);
  let navigate = useNavigate();
  const getCategories = async () => {
    const response = await fetch("https://idearanking.azurewebsites.net/api/ideas/categories");
    const deserializedJSON = await response.json();
    console.log(deserializedJSON["$values"])
    var tempCategories = deserializedJSON["$values"].map((category) => {
      return {value:category.id, label: category.name}
    })
    setCategories(tempCategories);
  }

  useEffect(() => {
 getCategories();

  },[])
  const handleChange = (e) => {
   var tempExisting = [];
   var tempNew = [];
   e.forEach(element => {
    if(element.__isNew__ === true)
    {
      tempNew.push(element.label);
    }
    else 
    {
      tempExisting.push(element.value);
    }
   });
   setExistigCategories(tempExisting);
   setNewCategories(tempNew);
  }
  
  const submitForm = async (e) => {
    e.preventDefault();
    let errors = false;
    if(e.target["title"].value === "")
    {
      errors = true;
      setTitleError("Title cannot be empty!");
      e.target["title"].classList.add("error");
    }
    if(e.target["description"].value === "")
    {
      errors = true;

      setDescriptionError("Description cannot be empty!");
      e.target["description"].classList.add("error");

    }
    if(errors === true) return;
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");
    const body = JSON.stringify({
      Title: e.target["title"].value,
      Description: e.target["description"].value,
      CategoryIds: existingCategories,
      NewCategories: newCategories
    })

    const requestOptions = {
      method: 'POST',
      mode: 'cors',
      headers: myHeaders,
      body: body,
      redirect: 'follow'
    };
    const response = await fetch("https://idearanking.azurewebsites.net/api/ideas", requestOptions)
    const deserializedJSON = await response.json();
    navigate("/Idea/"+deserializedJSON.id, { replace: true });
    // setData(deserializedJSON);
  }
  const removeErrors = (e) => {
    e.target.classList.remove("error")
  }
  return (
    <form className="create-form" onSubmit={submitForm} autoComplete="off">
      <label htmlFor="title">Title</label>
      <hr></hr>
      <input type={'text'} name='title' className='create__input' placeholder='Todo List' id="title" onChange={(e) => {removeErrors(e);setTitleError("")}}/>
      {titleError === "" ? <></> : <p className='error__msg'>{titleError}</p>}
      <label htmlFor="description">Description</label>
      <hr></hr>

      <textarea onChange={(e) => {removeErrors(e);setDescriptionError("")}} name="description" rows="4" cols="50" className='create__textarea' placeholder='Create a Todo List using React' id="description">
      </textarea>
      {descriptionError === "" ? <></> : <p className='error__msg'>{descriptionError}</p>}
      <label htmlFor='categories'>Categories</label>

      <div className='categories'>
        {/* TODO: Category */}
        <Creatable  isMulti
         onChange={handleChange}
        styles={customStyles}
        options={categories}
        theme={theme => ({
          ...theme,
          borderRadius: 8,
          colors: {
            ...theme.colors,
            primary25: '#202024',
            primary: 'black',
            neutral0: '#121214'
          },
        })}
        id='categories'
        />
       </div>
      <button type='submit'>Add Idea</button>

    </form>
  );
}

export default Form;

