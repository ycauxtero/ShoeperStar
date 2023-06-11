const colorPicker = document.querySelector("#colorPicker");
const txtColor = document.querySelector("#Color");
const txtColorHex = document.querySelector("#ColorHex");

const getColorValues = async (color) => {
	const uri = 'https://www.thecolorapi.com/id?hex=' + color.substring(1, color.length);

	const response = await fetch(uri);
	const data = await response.json();

	return {
		name: data.name.value,
		hexValue: data.name.closest_named_hex
	}
}

const setColor = async () => {
	const selectedColor = colorPicker.value;

	const colorObj = await getColorValues(selectedColor);

	//txtColor.value = colorObj.name;
	SetInputValue(txtColor, colorObj.name);

	//txtColorHex.value = colorObj.hexValue;
	SetInputValue(txtColorHex, colorObj.hexValue);

	colorPicker.value = colorObj.hexValue;
	colorPicker.style.backgroundColor = colorObj.hexValue;
}

function SetInputValue(inputElement, value) {
	inputElement.readOnly = false;
	inputElement.value = value;
	inputElement.readOnly = true;
}

colorPicker.addEventListener("input", setColor);
