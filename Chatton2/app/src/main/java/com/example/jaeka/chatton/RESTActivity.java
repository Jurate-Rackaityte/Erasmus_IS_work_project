package com.example.jaeka.chatton;

import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class RESTActivity extends AppCompatActivity {

    private Button btn_send, btn_refresh;
    private EditText input_Msg;
    private TextView chat_msgs;

    private String username;
    private String password;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_rest);

        btn_send = (Button) findViewById(R.id.btn_send);
        btn_refresh = (Button) findViewById(R.id.btn_refresh);
        input_Msg = (EditText) findViewById(R.id.input_Msg);
        chat_msgs = (TextView) findViewById(R.id.chat_msgs);

        Intent myIntent = getIntent();
        username = myIntent.getStringExtra("username");
        password = myIntent.getStringExtra("password");

        TextView info = (TextView) findViewById(R.id.info);
        info.setText("Logged in as user: " + username);

        RESTTask restTask = new RESTTask();
        restTask.execute();



        btn_refresh.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v){
                Intent intent = getIntent();
                finish();
                startActivity(intent);

            }
        }

        );
    }

    private  class RESTTask extends AsyncTask<Void, Void, String> {
        private static final String SERVICE_URL = "http://ascasda/Messages";  //here I need to put a real URL of where are the messages maybe?

        @Override
        protected String doInBackground(Void... params) {
            return null;
        }

        @Override
        protected void onPostExecute(String s) {
            super.onPostExecute(s);
        }
    }
}
