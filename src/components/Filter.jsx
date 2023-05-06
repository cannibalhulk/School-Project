import { useState } from 'react'
import '../styles/Filter.css'

function Filter() {

  let [filterVisible, setFilterVisible] = useState(false);

  let filterHandler = () => {
    if (!filterVisible) {
      setFilterVisible(true);
    } else {
      setFilterVisible(false);
    }
    console.log(filterVisible);
  }

  return (
    <>
      <div id="filter">
        <span onClick={filterHandler}>Filtrlə</span>
        {filterVisible && <div>
          <form action="">
            <label for="yazici">Yazıçı </label>
            <input type="text" name="yazici" id="" />
            <br/>
            <label for="il">İl </label>
            <input type="number" name="il" id=""/>
            <input type="submit" value="Filtrlə" />
          </form>
        </div>}

      </div>
    </>
  )
}

export default Filter
