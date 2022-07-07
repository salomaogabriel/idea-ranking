// import './App.css';
import './form.css';
import { useNavigate} from "react-router-dom";
import { useState } from 'react';
function Form() {
  const [titleError, setTitleError] = useState("");
  const [descriptionError, setDescriptionError] = useState("");
  let navigate = useNavigate();

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
      CategoryIds: [],
      NewCategories: []
    })
    const requestOptions = {
      method: 'POST',
      mode: 'cors',
      headers: myHeaders,
      body: body,
      redirect: 'follow'
    };
    const response = await fetch("https://localhost:7002/api/Ideas", requestOptions)
    const deserializedJSON = await response.json();
    navigate("/Idea/"+deserializedJSON.id, { replace: true });
    console.log(deserializedJSON);
    // setData(deserializedJSON);
  }
  const removeErrors = (e) => {
    e.target.classList.remove("error")
  }
  return (
    <form className="create-form" onSubmit={submitForm}>
      <label htmlFor="title">Title</label>
      <hr></hr>
      <input type={'text'} name='title' className='create__input' placeholder='Todo List' id="title" onChange={(e) => {removeErrors(e);setTitleError("")}}/>
      {titleError === "" ? <></> : <p className='error__msg'>{titleError}</p>}
      <label htmlFor="description">Description</label>
      <hr></hr>

      <textarea onChange={(e) => {removeErrors(e);setDescriptionError("")}} name="description" rows="4" cols="50" className='create__textarea' placeholder='Create a Todo List using React' id="description">
      </textarea>
      {descriptionError === "" ? <></> : <p className='error__msg'>{descriptionError}</p>}

        {/* TODO: Category */}
      <button type='submit'>Add Idea</button>

    </form>
  );
}

export default Form;

