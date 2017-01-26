package com.example.jaeka.chatton;

import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.widget.TextView;

public class RESTActivity extends AppCompatActivity {
    private String username;
    private String password;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_rest);

        Intent myIntent = getIntent();
        username = myIntent.getStringExtra("username");
        password = myIntent.getStringExtra("password");

        TextView info = (TextView) findViewById(R.id.info);
        info.setText("Logged in as user: " + username);

        RESTTask restTask = new RESTTask();
        restTask.execute();
    }

    private  class RESTTask extends AsyncTask<Void, Void, String> {
        private static final String SERVICE_URL = "http://ascasda/Messages";

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
